using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MagtekCardReader
{
    class Program
    {
        private const int VendorId = 0x0801;
        private const int ProductId = 0x0002;

        private static HidDevice _device;

        static void Main()
        {
            _device = HidDevices.Enumerate(VendorId, ProductId).FirstOrDefault();

            if (_device != null)
            {
                _device.OpenDevice();

                _device.Inserted += DeviceAttachedHandler;
                _device.Removed += DeviceRemovedHandler;

                _device.MonitorDeviceEvents = true;

                _device.ReadReport(OnReport);

                Console.WriteLine("Reader found, press any key to exit.");
                Console.ReadKey();

                _device.CloseDevice();
            }
            else
            {
                Console.WriteLine("Could not find reader.");
                Console.ReadKey();
            }
        }

        private static void OnReport(HidReport report)
        {
            if (!_device.IsConnected) { return; }

            var cardData = new Data(report.Data);

            Console.WriteLine(!cardData.Error ? Encoding.ASCII.GetString(cardData.CardData) : cardData.ErrorMessage);

            _device.ReadReport(OnReport);
        }

        private static void DeviceAttachedHandler()
        {
            Console.WriteLine("Device attached.");
            _device.ReadReport(OnReport);
        }

        private static void DeviceRemovedHandler()
        {
            Console.WriteLine("Device removed.");
        }
    }
    public class HidDevice : IHidDevice
    {
        public event InsertedEventHandler Inserted;
        public event RemovedEventHandler Removed;

        private readonly string _description;
        private readonly string _devicePath;
        private readonly HidDeviceAttributes _deviceAttributes;

        private readonly HidDeviceCapabilities _deviceCapabilities;
        private DeviceMode _deviceReadMode = DeviceMode.NonOverlapped;
        private DeviceMode _deviceWriteMode = DeviceMode.NonOverlapped;
        private ShareMode _deviceShareMode = ShareMode.ShareRead | ShareMode.ShareWrite;

        private readonly HidDeviceEventMonitor _deviceEventMonitor;

        private bool _monitorDeviceEvents;
        protected delegate HidDeviceData ReadDelegate(int timeout);
        protected delegate HidReport ReadReportDelegate(int timeout);
        private delegate bool WriteDelegate(byte[] data, int timeout);
        private delegate bool WriteReportDelegate(HidReport report, int timeout);

        internal HidDevice(string devicePath, string description = null)
        {
            _deviceEventMonitor = new HidDeviceEventMonitor(this);
            _deviceEventMonitor.Inserted += DeviceEventMonitorInserted;
            _deviceEventMonitor.Removed += DeviceEventMonitorRemoved;

            _devicePath = devicePath;
            _description = description;

            try
            {
                var hidHandle = OpenDeviceIO(_devicePath, NativeMethods.ACCESS_NONE);

                _deviceAttributes = GetDeviceAttributes(hidHandle);
                _deviceCapabilities = GetDeviceCapabilities(hidHandle);

                CloseDeviceIO(hidHandle);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error querying HID device '{0}'.", devicePath), exception);
            }
        }

        public IntPtr Handle { get; private set; }
        public bool IsOpen { get; private set; }
        public bool IsConnected { get { return HidDevices.IsConnected(_devicePath); } }
        public string Description { get { return _description; } }
        public HidDeviceCapabilities Capabilities { get { return _deviceCapabilities; } }
        public HidDeviceAttributes Attributes { get { return _deviceAttributes; } }
        public string DevicePath { get { return _devicePath; } }

        public bool MonitorDeviceEvents
        {
            get { return _monitorDeviceEvents; }
            set
            {
                if (value & _monitorDeviceEvents == false) _deviceEventMonitor.Init();
                _monitorDeviceEvents = value;
            }
        }

        public override string ToString()
        {
            return string.Format("VendorID={0}, ProductID={1}, Version={2}, DevicePath={3}",
                                _deviceAttributes.VendorHexId,
                                _deviceAttributes.ProductHexId,
                                _deviceAttributes.Version,
                                _devicePath);
        }

        public void OpenDevice()
        {
            OpenDevice(DeviceMode.NonOverlapped, DeviceMode.NonOverlapped, ShareMode.ShareRead | ShareMode.ShareWrite);
        }

        public void OpenDevice(DeviceMode readMode, DeviceMode writeMode, ShareMode shareMode)
        {
            if (IsOpen) return;

            _deviceReadMode = readMode;
            _deviceWriteMode = writeMode;
            _deviceShareMode = shareMode;

            try
            {
                Handle = OpenDeviceIO(_devicePath, readMode, NativeMethods.GENERIC_READ | NativeMethods.GENERIC_WRITE, shareMode);
            }
            catch (Exception exception)
            {
                IsOpen = false;
                throw new Exception("Error opening HID device.", exception);
            }

            IsOpen = Handle.ToInt32() != NativeMethods.INVALID_HANDLE_VALUE;
        }


        public void CloseDevice()
        {
            if (!IsOpen) return;
            CloseDeviceIO(Handle);
            IsOpen = false;
        }

        public HidDeviceData Read()
        {
            return Read(0);
        }

        public HidDeviceData Read(int timeout)
        {
            if (IsConnected)
            {
                if (IsOpen == false) OpenDevice(_deviceReadMode, _deviceWriteMode, _deviceShareMode);
                try
                {
                    return ReadData(timeout);
                }
                catch
                {
                    return new HidDeviceData(HidDeviceData.ReadStatus.ReadError);
                }

            }
            return new HidDeviceData(HidDeviceData.ReadStatus.NotConnected);
        }

        public void Read(ReadCallback callback)
        {
            Read(callback, 0);
        }

        public void Read(ReadCallback callback, int timeout)
        {
            var readDelegate = new ReadDelegate(Read);
            var asyncState = new HidAsyncState(readDelegate, callback);
            readDelegate.BeginInvoke(timeout, EndRead, asyncState);
        }

        public async Task<HidDeviceData> ReadAsync(int timeout = 0)
        {
            //            var readDelegate = new ReadDelegate(Read);
            //#if NET20 || NET35
            //            return await Task<HidDeviceData>.Factory.StartNew(() => readDelegate.Invoke(timeout));
            //#else
            //            return await Task<HidDeviceData>.Factory.FromAsync(readDelegate.BeginInvoke, readDelegate.EndInvoke, timeout, null);
            //#endif
            HidDeviceData ss = new HidDeviceData(HidDeviceData.ReadStatus.Success);
            return ss;
        }

        public HidReport ReadReport()
        {
            return ReadReport(0);
        }

        public HidReport ReadReport(int timeout)
        {
            return new HidReport(Capabilities.InputReportByteLength, Read(timeout));
        }

        public void ReadReport(ReadReportCallback callback)
        {
            ReadReport(callback, 0);
        }

        public void ReadReport(ReadReportCallback callback, int timeout)
        {
            var readReportDelegate = new ReadReportDelegate(ReadReport);
            var asyncState = new HidAsyncState(readReportDelegate, callback);
            readReportDelegate.BeginInvoke(timeout, EndReadReport, asyncState);
        }

        public async Task<HidReport> ReadReportAsync(int timeout = 0)
        {
            //            var readReportDelegate = new ReadReportDelegate(ReadReport);
            //#if NET20 || NET35
            //            return await Task<HidReport>.Factory.StartNew(() => readReportDelegate.Invoke(timeout));
            //#else
            //            return await Task<HidReport>.Factory.FromAsync(readReportDelegate.BeginInvoke, readReportDelegate.EndInvoke, timeout, null);
            //#endif
            HidReport zec = new HidReport(6);
            return zec;
        }

        /// <summary>
        /// Reads an input report from the Control channel.  This method provides access to report data for devices that 
        /// do not use the interrupt channel to communicate for specific usages.
        /// </summary>
        /// <param name="reportId">The report ID to read from the device</param>
        /// <returns>The HID report that is read.  The report will contain the success status of the read request</returns>
        /// 
        public HidReport ReadReportSync(byte reportId)
        {
            byte[] cmdBuffer = new byte[Capabilities.InputReportByteLength];
            cmdBuffer[0] = reportId;
            bool bSuccess = NativeMethods.HidD_GetInputReport(Handle, cmdBuffer, cmdBuffer.Length);
            HidDeviceData deviceData = new HidDeviceData(cmdBuffer, bSuccess ? HidDeviceData.ReadStatus.Success : HidDeviceData.ReadStatus.NoDataRead);
            return new HidReport(Capabilities.InputReportByteLength, deviceData);
        }

        public bool ReadFeatureData(out byte[] data, byte reportId = 0)
        {
            if (_deviceCapabilities.FeatureReportByteLength <= 0)
            {
                data = new byte[0];
                return false;
            }

            data = new byte[_deviceCapabilities.FeatureReportByteLength];

            var buffer = CreateFeatureOutputBuffer();
            buffer[0] = reportId;

            IntPtr hidHandle = IntPtr.Zero;
            bool success = false;
            try
            {
                if (IsOpen)
                    hidHandle = Handle;
                else
                    hidHandle = OpenDeviceIO(_devicePath, NativeMethods.ACCESS_NONE);

                success = NativeMethods.HidD_GetFeature(hidHandle, buffer, buffer.Length);

                if (success)
                {
                    Array.Copy(buffer, 0, data, 0, Math.Min(data.Length, _deviceCapabilities.FeatureReportByteLength));
                }
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error accessing HID device '{0}'.", _devicePath), exception);
            }
            finally
            {
                if (hidHandle != IntPtr.Zero && hidHandle != Handle)
                    CloseDeviceIO(hidHandle);
            }

            return success;
        }

        public bool ReadProduct(out byte[] data)
        {
            data = new byte[254];
            IntPtr hidHandle = IntPtr.Zero;
            bool success = false;
            try
            {
                if (IsOpen)
                    hidHandle = Handle;
                else
                    hidHandle = OpenDeviceIO(_devicePath, NativeMethods.ACCESS_NONE);

                success = NativeMethods.HidD_GetProductString(hidHandle, ref data[0], data.Length);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error accessing HID device '{0}'.", _devicePath), exception);
            }
            finally
            {
                if (hidHandle != IntPtr.Zero && hidHandle != Handle)
                    CloseDeviceIO(hidHandle);
            }

            return success;
        }

        public bool ReadManufacturer(out byte[] data)
        {
            data = new byte[254];
            IntPtr hidHandle = IntPtr.Zero;
            bool success = false;
            try
            {
                if (IsOpen)
                    hidHandle = Handle;
                else
                    hidHandle = OpenDeviceIO(_devicePath, NativeMethods.ACCESS_NONE);

                success = NativeMethods.HidD_GetManufacturerString(hidHandle, ref data[0], data.Length);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error accessing HID device '{0}'.", _devicePath), exception);
            }
            finally
            {
                if (hidHandle != IntPtr.Zero && hidHandle != Handle)
                    CloseDeviceIO(hidHandle);
            }

            return success;
        }

        public bool ReadSerialNumber(out byte[] data)
        {
            data = new byte[254];
            IntPtr hidHandle = IntPtr.Zero;
            bool success = false;
            try
            {
                if (IsOpen)
                    hidHandle = Handle;
                else
                    hidHandle = OpenDeviceIO(_devicePath, NativeMethods.ACCESS_NONE);

                success = NativeMethods.HidD_GetSerialNumberString(hidHandle, ref data[0], data.Length);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error accessing HID device '{0}'.", _devicePath), exception);
            }
            finally
            {
                if (hidHandle != IntPtr.Zero && hidHandle != Handle)
                    CloseDeviceIO(hidHandle);
            }

            return success;
        }

        public bool Write(byte[] data)
        {
            return Write(data, 0);
        }

        public bool Write(byte[] data, int timeout)
        {
            if (IsConnected)
            {
                if (IsOpen == false) OpenDevice(_deviceReadMode, _deviceWriteMode, _deviceShareMode);
                try
                {
                    return WriteData(data, timeout);
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public void Write(byte[] data, WriteCallback callback)
        {
            Write(data, callback, 0);
        }

        public void Write(byte[] data, WriteCallback callback, int timeout)
        {
            var writeDelegate = new WriteDelegate(Write);
            var asyncState = new HidAsyncState(writeDelegate, callback);
            writeDelegate.BeginInvoke(data, timeout, EndWrite, asyncState);
        }

        public async Task<bool> WriteAsync(byte[] data, int timeout = 0)
        {
//            var writeDelegate = new WriteDelegate(Write);
//#if NET20 || NET35
//            return await Task<bool>.Factory.StartNew(() => writeDelegate.Invoke(data, timeout));
//#else
//            return await Task<bool>.Factory.FromAsync(writeDelegate.BeginInvoke, writeDelegate.EndInvoke, data, timeout, null);
//#endif
            return true;
        }

        public bool WriteReport(HidReport report)
        {
            return WriteReport(report, 0);
        }

        public bool WriteReport(HidReport report, int timeout)
        {
            return Write(report.GetBytes(), timeout);
        }

        public void WriteReport(HidReport report, WriteCallback callback)
        {
            WriteReport(report, callback, 0);
        }

        public void WriteReport(HidReport report, WriteCallback callback, int timeout)
        {
            var writeReportDelegate = new WriteReportDelegate(WriteReport);
            var asyncState = new HidAsyncState(writeReportDelegate, callback);
            writeReportDelegate.BeginInvoke(report, timeout, EndWriteReport, asyncState);
        }

        /// <summary>
        /// Handle data transfers on the control channel.  This method places data on the control channel for devices
        /// that do not support the interupt transfers
        /// </summary>
        /// <param name="report">The outbound HID report</param>
        /// <returns>The result of the tranfer request: true if successful otherwise false</returns>
        /// 
        public bool WriteReportSync(HidReport report)
        {

            if (null != report)
            {
                byte[] buffer = report.GetBytes();
                return (NativeMethods.HidD_SetOutputReport(Handle, buffer, buffer.Length));
            }
            else
                throw new ArgumentException("The output report is null, it must be allocated before you call this method", "report");
        }

        public async Task<bool> WriteReportAsync(HidReport report, int timeout = 0)
        {
//            var writeReportDelegate = new WriteReportDelegate(WriteReport);
//#if NET20 || NET35
//            return await Task<bool>.Factory.StartNew(() => writeReportDelegate.Invoke(report, timeout));
//#else
//            return await Task<bool>.Factory.FromAsync(writeReportDelegate.BeginInvoke, writeReportDelegate.EndInvoke, report, timeout, null);
//#endif
            return true;
        }

        public HidReport CreateReport()
        {
            return new HidReport(Capabilities.OutputReportByteLength);
        }

        public bool WriteFeatureData(byte[] data)
        {
            if (_deviceCapabilities.FeatureReportByteLength <= 0) return false;

            var buffer = CreateFeatureOutputBuffer();

            Array.Copy(data, 0, buffer, 0, Math.Min(data.Length, _deviceCapabilities.FeatureReportByteLength));


            IntPtr hidHandle = IntPtr.Zero;
            bool success = false;
            try
            {
                if (IsOpen)
                    hidHandle = Handle;
                else
                    hidHandle = OpenDeviceIO(_devicePath, NativeMethods.ACCESS_NONE);

                //var overlapped = new NativeOverlapped();
                success = NativeMethods.HidD_SetFeature(hidHandle, buffer, buffer.Length);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error accessing HID device '{0}'.", _devicePath), exception);
            }
            finally
            {
                if (hidHandle != IntPtr.Zero && hidHandle != Handle)
                    CloseDeviceIO(hidHandle);
            }
            return success;
        }

        protected static void EndRead(IAsyncResult ar)
        {
            var hidAsyncState = (HidAsyncState)ar.AsyncState;
            var callerDelegate = (ReadDelegate)hidAsyncState.CallerDelegate;
            var callbackDelegate = (ReadCallback)hidAsyncState.CallbackDelegate;
            var data = callerDelegate.EndInvoke(ar);

            if ((callbackDelegate != null)) callbackDelegate.Invoke(data);
        }

        protected static void EndReadReport(IAsyncResult ar)
        {
            var hidAsyncState = (HidAsyncState)ar.AsyncState;
            var callerDelegate = (ReadReportDelegate)hidAsyncState.CallerDelegate;
            var callbackDelegate = (ReadReportCallback)hidAsyncState.CallbackDelegate;
            var report = callerDelegate.EndInvoke(ar);

            if ((callbackDelegate != null)) callbackDelegate.Invoke(report);
        }

        private static void EndWrite(IAsyncResult ar)
        {
            var hidAsyncState = (HidAsyncState)ar.AsyncState;
            var callerDelegate = (WriteDelegate)hidAsyncState.CallerDelegate;
            var callbackDelegate = (WriteCallback)hidAsyncState.CallbackDelegate;
            var result = callerDelegate.EndInvoke(ar);

            if ((callbackDelegate != null)) callbackDelegate.Invoke(result);
        }

        private static void EndWriteReport(IAsyncResult ar)
        {
            var hidAsyncState = (HidAsyncState)ar.AsyncState;
            var callerDelegate = (WriteReportDelegate)hidAsyncState.CallerDelegate;
            var callbackDelegate = (WriteCallback)hidAsyncState.CallbackDelegate;
            var result = callerDelegate.EndInvoke(ar);

            if ((callbackDelegate != null)) callbackDelegate.Invoke(result);
        }

        private byte[] CreateInputBuffer()
        {
            return CreateBuffer(Capabilities.InputReportByteLength - 1);
        }

        private byte[] CreateOutputBuffer()
        {
            return CreateBuffer(Capabilities.OutputReportByteLength - 1);
        }

        private byte[] CreateFeatureOutputBuffer()
        {
            return CreateBuffer(Capabilities.FeatureReportByteLength - 1);
        }

        private static byte[] CreateBuffer(int length)
        {
            byte[] buffer = null;
            Array.Resize(ref buffer, length + 1);
            return buffer;
        }

        private static HidDeviceAttributes GetDeviceAttributes(IntPtr hidHandle)
        {
            var deviceAttributes = default(NativeMethods.HIDD_ATTRIBUTES);
            deviceAttributes.Size = Marshal.SizeOf(deviceAttributes);
            NativeMethods.HidD_GetAttributes(hidHandle, ref deviceAttributes);
            return new HidDeviceAttributes(deviceAttributes);
        }

        private static HidDeviceCapabilities GetDeviceCapabilities(IntPtr hidHandle)
        {
            var capabilities = default(NativeMethods.HIDP_CAPS);
            var preparsedDataPointer = default(IntPtr);

            if (NativeMethods.HidD_GetPreparsedData(hidHandle, ref preparsedDataPointer))
            {
                NativeMethods.HidP_GetCaps(preparsedDataPointer, ref capabilities);
                NativeMethods.HidD_FreePreparsedData(preparsedDataPointer);
            }
            return new HidDeviceCapabilities(capabilities);
        }

        private bool WriteData(byte[] data, int timeout)
        {
            if (_deviceCapabilities.OutputReportByteLength <= 0) return false;

            var buffer = CreateOutputBuffer();
            uint bytesWritten = 0;

            Array.Copy(data, 0, buffer, 0, Math.Min(data.Length, _deviceCapabilities.OutputReportByteLength));

            if (_deviceWriteMode == DeviceMode.Overlapped)
            {
                var security = new NativeMethods.SECURITY_ATTRIBUTES();
                var overlapped = new NativeOverlapped();

                var overlapTimeout = timeout <= 0 ? NativeMethods.WAIT_INFINITE : timeout;

                security.lpSecurityDescriptor = IntPtr.Zero;
                security.bInheritHandle = true;
                security.nLength = Marshal.SizeOf(security);

                overlapped.OffsetLow = 0;
                overlapped.OffsetHigh = 0;
                overlapped.EventHandle = NativeMethods.CreateEvent(ref security, Convert.ToInt32(false), Convert.ToInt32(true), "");

                try
                {
                    NativeMethods.WriteFile(Handle, buffer, (uint)buffer.Length, out bytesWritten, ref overlapped);
                }
                catch { return false; }

                var result = NativeMethods.WaitForSingleObject(overlapped.EventHandle, overlapTimeout);

                switch (result)
                {
                    case NativeMethods.WAIT_OBJECT_0:
                        return true;
                    case NativeMethods.WAIT_TIMEOUT:
                        return false;
                    case NativeMethods.WAIT_FAILED:
                        return false;
                    default:
                        return false;
                }
            }
            else
            {
                try
                {
                    var overlapped = new NativeOverlapped();
                    return NativeMethods.WriteFile(Handle, buffer, (uint)buffer.Length, out bytesWritten, ref overlapped);
                }
                catch { return false; }
            }
        }

        protected HidDeviceData ReadData(int timeout)
        {
            var buffer = new byte[] { };
            var status = HidDeviceData.ReadStatus.NoDataRead;
            IntPtr nonManagedBuffer;

            if (_deviceCapabilities.InputReportByteLength > 0)
            {
                uint bytesRead = 0;

                buffer = CreateInputBuffer();
                nonManagedBuffer = Marshal.AllocHGlobal(buffer.Length);

                if (_deviceReadMode == DeviceMode.Overlapped)
                {
                    var security = new NativeMethods.SECURITY_ATTRIBUTES();
                    var overlapped = new NativeOverlapped();
                    var overlapTimeout = timeout <= 0 ? NativeMethods.WAIT_INFINITE : timeout;

                    security.lpSecurityDescriptor = IntPtr.Zero;
                    security.bInheritHandle = true;
                    security.nLength = Marshal.SizeOf(security);

                    overlapped.OffsetLow = 0;
                    overlapped.OffsetHigh = 0;
                    overlapped.EventHandle = NativeMethods.CreateEvent(ref security, Convert.ToInt32(false), Convert.ToInt32(true), string.Empty);

                    try
                    {
                        var success = NativeMethods.ReadFile(Handle, nonManagedBuffer, (uint)buffer.Length, out bytesRead, ref overlapped);

                        if (success)
                        {
                            status = HidDeviceData.ReadStatus.Success; // No check here to see if bytesRead > 0 . Perhaps not necessary?
                        }
                        else
                        {
                            var result = NativeMethods.WaitForSingleObject(overlapped.EventHandle, overlapTimeout);

                            switch (result)
                            {
                                case NativeMethods.WAIT_OBJECT_0:
                                    status = HidDeviceData.ReadStatus.Success;
                                    NativeMethods.GetOverlappedResult(Handle, ref overlapped, out bytesRead, false);
                                    break;
                                case NativeMethods.WAIT_TIMEOUT:
                                    status = HidDeviceData.ReadStatus.WaitTimedOut;
                                    buffer = new byte[] { };
                                    break;
                                case NativeMethods.WAIT_FAILED:
                                    status = HidDeviceData.ReadStatus.WaitFail;
                                    buffer = new byte[] { };
                                    break;
                                default:
                                    status = HidDeviceData.ReadStatus.NoDataRead;
                                    buffer = new byte[] { };
                                    break;
                            }
                        }
                        Marshal.Copy(nonManagedBuffer, buffer, 0, (int)bytesRead);
                    }
                    catch { status = HidDeviceData.ReadStatus.ReadError; }
                    finally
                    {
                        CloseDeviceIO(overlapped.EventHandle);
                        Marshal.FreeHGlobal(nonManagedBuffer);
                    }
                }
                else
                {
                    try
                    {
                        var overlapped = new NativeOverlapped();

                        NativeMethods.ReadFile(Handle, nonManagedBuffer, (uint)buffer.Length, out bytesRead, ref overlapped);
                        status = HidDeviceData.ReadStatus.Success;
                        Marshal.Copy(nonManagedBuffer, buffer, 0, (int)bytesRead);
                    }
                    catch { status = HidDeviceData.ReadStatus.ReadError; }
                    finally { Marshal.FreeHGlobal(nonManagedBuffer); }
                }
            }
            return new HidDeviceData(buffer, status);
        }

        private static IntPtr OpenDeviceIO(string devicePath, uint deviceAccess)
        {
            return OpenDeviceIO(devicePath, DeviceMode.NonOverlapped, deviceAccess, ShareMode.ShareRead | ShareMode.ShareWrite);
        }

        private static IntPtr OpenDeviceIO(string devicePath, DeviceMode deviceMode, uint deviceAccess, ShareMode shareMode)
        {
            var security = new NativeMethods.SECURITY_ATTRIBUTES();
            var flags = 0;

            if (deviceMode == DeviceMode.Overlapped) flags = NativeMethods.FILE_FLAG_OVERLAPPED;

            security.lpSecurityDescriptor = IntPtr.Zero;
            security.bInheritHandle = true;
            security.nLength = Marshal.SizeOf(security);

            return NativeMethods.CreateFile(devicePath, deviceAccess, (int)shareMode, ref security, NativeMethods.OPEN_EXISTING, flags, 0);
        }

        private static void CloseDeviceIO(IntPtr handle)
        {
            if (Environment.OSVersion.Version.Major > 5)
            {
                NativeMethods.CancelIoEx(handle, IntPtr.Zero);
            }
            NativeMethods.CloseHandle(handle);
        }

        private void DeviceEventMonitorInserted()
        {
            if (!IsOpen) OpenDevice(_deviceReadMode, _deviceWriteMode, _deviceShareMode);
            if (Inserted != null) Inserted();
        }

        private void DeviceEventMonitorRemoved()
        {
            if (IsOpen) CloseDevice();
            if (Removed != null) Removed();
        }

        public void Dispose()
        {
            if (MonitorDeviceEvents) MonitorDeviceEvents = false;
            if (IsOpen) CloseDevice();
        }
    }
    public static class Extensions
    {
        public static string ToUTF8String(this byte[] buffer)
        {
            var value = Encoding.UTF8.GetString(buffer);
            return value.Remove(value.IndexOf((char)0));
        }

        public static string ToUTF16String(this byte[] buffer)
        {
            var value = Encoding.Unicode.GetString(buffer);
            return value.Remove(value.IndexOf((char)0));
        }
    }
    public class HidAsyncState
    {
        private readonly object _callerDelegate;
        private readonly object _callbackDelegate;

        public HidAsyncState(object callerDelegate, object callbackDelegate)
        {
            _callerDelegate = callerDelegate;
            _callbackDelegate = callbackDelegate;
        }

        public object CallerDelegate { get { return _callerDelegate; } }
        public object CallbackDelegate { get { return _callbackDelegate; } }
    }
    public class HidDeviceAttributes
    {
        internal HidDeviceAttributes(NativeMethods.HIDD_ATTRIBUTES attributes)
        {
            VendorId = attributes.VendorID;
            ProductId = attributes.ProductID;
            Version = attributes.VersionNumber;

            VendorHexId = "0x" + attributes.VendorID.ToString("X4");
            ProductHexId = "0x" + attributes.ProductID.ToString("X4");
        }

        public int VendorId { get; private set; }
        public int ProductId { get; private set; }
        public int Version { get; private set; }
        public string VendorHexId { get; set; }
        public string ProductHexId { get; set; }
    }
    public class HidDeviceCapabilities
    {
        internal HidDeviceCapabilities(NativeMethods.HIDP_CAPS capabilities)
        {
            Usage = capabilities.Usage;
            UsagePage = capabilities.UsagePage;
            InputReportByteLength = capabilities.InputReportByteLength;
            OutputReportByteLength = capabilities.OutputReportByteLength;
            FeatureReportByteLength = capabilities.FeatureReportByteLength;
            Reserved = capabilities.Reserved;
            NumberLinkCollectionNodes = capabilities.NumberLinkCollectionNodes;
            NumberInputButtonCaps = capabilities.NumberInputButtonCaps;
            NumberInputValueCaps = capabilities.NumberInputValueCaps;
            NumberInputDataIndices = capabilities.NumberInputDataIndices;
            NumberOutputButtonCaps = capabilities.NumberOutputButtonCaps;
            NumberOutputValueCaps = capabilities.NumberOutputValueCaps;
            NumberOutputDataIndices = capabilities.NumberOutputDataIndices;
            NumberFeatureButtonCaps = capabilities.NumberFeatureButtonCaps;
            NumberFeatureValueCaps = capabilities.NumberFeatureValueCaps;
            NumberFeatureDataIndices = capabilities.NumberFeatureDataIndices;

        }

        public short Usage { get; private set; }
        public short UsagePage { get; private set; }
        public short InputReportByteLength { get; private set; }
        public short OutputReportByteLength { get; private set; }
        public short FeatureReportByteLength { get; private set; }
        public short[] Reserved { get; private set; }
        public short NumberLinkCollectionNodes { get; private set; }
        public short NumberInputButtonCaps { get; private set; }
        public short NumberInputValueCaps { get; private set; }
        public short NumberInputDataIndices { get; private set; }
        public short NumberOutputButtonCaps { get; private set; }
        public short NumberOutputValueCaps { get; private set; }
        public short NumberOutputDataIndices { get; private set; }
        public short NumberFeatureButtonCaps { get; private set; }
        public short NumberFeatureValueCaps { get; private set; }
        public short NumberFeatureDataIndices { get; private set; }
    }
    public class HidDeviceData
    {
        public enum ReadStatus
        {
            Success = 0,
            WaitTimedOut = 1,
            WaitFail = 2,
            NoDataRead = 3,
            ReadError = 4,
            NotConnected = 5
        }

        public HidDeviceData(ReadStatus status)
        {
            Data = new byte[] { };
            Status = status;
        }

        public HidDeviceData(byte[] data, ReadStatus status)
        {
            Data = data;
            Status = status;
        }

        public byte[] Data { get; private set; }
        public ReadStatus Status { get; private set; }
    }
    internal class HidDeviceEventMonitor
    {
        public event InsertedEventHandler Inserted;
        public event RemovedEventHandler Removed;

        public delegate void InsertedEventHandler();
        public delegate void RemovedEventHandler();

        private readonly HidDevice _device;
        private bool _wasConnected;

        public HidDeviceEventMonitor(HidDevice device)
        {
            _device = device;
        }

        public void Init()
        {
            var eventMonitor = new Action(DeviceEventMonitor);
            eventMonitor.BeginInvoke(DisposeDeviceEventMonitor, eventMonitor);
        }

        private void DeviceEventMonitor()
        {
            var isConnected = _device.IsConnected;

            if (isConnected != _wasConnected)
            {
                if (isConnected && Inserted != null) Inserted();
                else if (!isConnected && Removed != null) Removed();
                _wasConnected = isConnected;
            }

            Thread.Sleep(500);

            if (_device.MonitorDeviceEvents) Init();
        }

        private static void DisposeDeviceEventMonitor(IAsyncResult ar)
        {
            ((Action)ar.AsyncState).EndInvoke(ar);
        }
    }
    public class HidDevices
    {
        private static Guid _hidClassGuid = Guid.Empty;

        public static bool IsConnected(string devicePath)
        {
            return EnumerateDevices().Any(x => x.Path == devicePath);
        }

        public static HidDevice GetDevice(string devicePath)
        {
            return Enumerate(devicePath).FirstOrDefault();
        }

        public static IEnumerable<HidDevice> Enumerate()
        {
            return EnumerateDevices().Select(x => new HidDevice(x.Path, x.Description));
        }

        public static IEnumerable<HidDevice> Enumerate(string devicePath)
        {
            return EnumerateDevices().Where(x => x.Path == devicePath).Select(x => new HidDevice(x.Path, x.Description));
        }

        public static IEnumerable<HidDevice> Enumerate(int vendorId, params int[] productIds)
        {
            return EnumerateDevices().Select(x => new HidDevice(x.Path, x.Description)).Where(x => x.Attributes.VendorId == vendorId &&
                                                                                  productIds.Contains(x.Attributes.ProductId));
        }

        public static IEnumerable<HidDevice> Enumerate(int vendorId, int productId, ushort UsagePage)
        {
            return EnumerateDevices().Select(x => new HidDevice(x.Path, x.Description)).Where(x => x.Attributes.VendorId == vendorId &&
                                                                                  productId == (ushort)x.Attributes.ProductId && (ushort)x.Capabilities.UsagePage == UsagePage);
        }
        public static IEnumerable<HidDevice> Enumerate(int vendorId)
        {
            return EnumerateDevices().Select(x => new HidDevice(x.Path, x.Description)).Where(x => x.Attributes.VendorId == vendorId);
        }

        internal class DeviceInfo { public string Path { get; set; } public string Description { get; set; } }

        internal static IEnumerable<DeviceInfo> EnumerateDevices()
        {
            var devices = new List<DeviceInfo>();
            var hidClass = HidClassGuid;
            var deviceInfoSet = NativeMethods.SetupDiGetClassDevs(ref hidClass, null, 0, NativeMethods.DIGCF_PRESENT | NativeMethods.DIGCF_DEVICEINTERFACE);

            if (deviceInfoSet.ToInt64() != NativeMethods.INVALID_HANDLE_VALUE)
            {
                var deviceInfoData = CreateDeviceInfoData();
                var deviceIndex = 0;

                while (NativeMethods.SetupDiEnumDeviceInfo(deviceInfoSet, deviceIndex, ref deviceInfoData))
                {
                    deviceIndex += 1;

                    var deviceInterfaceData = new NativeMethods.SP_DEVICE_INTERFACE_DATA();
                    deviceInterfaceData.cbSize = Marshal.SizeOf(deviceInterfaceData);
                    var deviceInterfaceIndex = 0;

                    while (NativeMethods.SetupDiEnumDeviceInterfaces(deviceInfoSet, ref deviceInfoData, ref hidClass, deviceInterfaceIndex, ref deviceInterfaceData))
                    {
                        deviceInterfaceIndex++;
                        var devicePath = GetDevicePath(deviceInfoSet, deviceInterfaceData);
                        var description = GetBusReportedDeviceDescription(deviceInfoSet, ref deviceInfoData) ??
                                          GetDeviceDescription(deviceInfoSet, ref deviceInfoData);
                        devices.Add(new DeviceInfo { Path = devicePath, Description = description });
                    }
                }
                NativeMethods.SetupDiDestroyDeviceInfoList(deviceInfoSet);
            }
            return devices;
        }

        private static NativeMethods.SP_DEVINFO_DATA CreateDeviceInfoData()
        {
            var deviceInfoData = new NativeMethods.SP_DEVINFO_DATA();

            deviceInfoData.cbSize = Marshal.SizeOf(deviceInfoData);
            deviceInfoData.DevInst = 0;
            deviceInfoData.ClassGuid = Guid.Empty;
            deviceInfoData.Reserved = IntPtr.Zero;

            return deviceInfoData;
        }

        private static string GetDevicePath(IntPtr deviceInfoSet, NativeMethods.SP_DEVICE_INTERFACE_DATA deviceInterfaceData)
        {
            var bufferSize = 0;
            var interfaceDetail = new NativeMethods.SP_DEVICE_INTERFACE_DETAIL_DATA { Size = IntPtr.Size == 4 ? 4 + Marshal.SystemDefaultCharSize : 8 };

            NativeMethods.SetupDiGetDeviceInterfaceDetailBuffer(deviceInfoSet, ref deviceInterfaceData, IntPtr.Zero, 0, ref bufferSize, IntPtr.Zero);

            return NativeMethods.SetupDiGetDeviceInterfaceDetail(deviceInfoSet, ref deviceInterfaceData, ref interfaceDetail, bufferSize, ref bufferSize, IntPtr.Zero) ?
                interfaceDetail.DevicePath : null;
        }

        private static Guid HidClassGuid
        {
            get
            {
                if (_hidClassGuid.Equals(Guid.Empty)) NativeMethods.HidD_GetHidGuid(ref _hidClassGuid);
                return _hidClassGuid;
            }
        }

        private static string GetDeviceDescription(IntPtr deviceInfoSet, ref NativeMethods.SP_DEVINFO_DATA devinfoData)
        {
            var descriptionBuffer = new byte[1024];

            var requiredSize = 0;
            var type = 0;

            NativeMethods.SetupDiGetDeviceRegistryProperty(deviceInfoSet,
                                                            ref devinfoData,
                                                            NativeMethods.SPDRP_DEVICEDESC,
                                                            ref type,
                                                            descriptionBuffer,
                                                            descriptionBuffer.Length,
                                                            ref requiredSize);

            return descriptionBuffer.ToUTF8String();
        }

        private static string GetBusReportedDeviceDescription(IntPtr deviceInfoSet, ref NativeMethods.SP_DEVINFO_DATA devinfoData)
        {
            var descriptionBuffer = new byte[1024];

            if (Environment.OSVersion.Version.Major > 5)
            {
                ulong propertyType = 0;
                var requiredSize = 0;

                var _continue = NativeMethods.SetupDiGetDeviceProperty(deviceInfoSet,
                                                                        ref devinfoData,
                                                                        ref NativeMethods.DEVPKEY_Device_BusReportedDeviceDesc,
                                                                        ref propertyType,
                                                                        descriptionBuffer,
                                                                        descriptionBuffer.Length,
                                                                        ref requiredSize,
                                                                        0);

                if (_continue) return descriptionBuffer.ToUTF16String();
            }
            return null;
        }
    }
    public class HidFastReadDevice : HidDevice
    {
        internal HidFastReadDevice(string devicePath, string description = null)
            : base(devicePath, description) { }

        // FastRead assumes that the device is connected,
        // which could cause stability issues if hardware is
        // disconnected during a read
        public HidDeviceData FastRead()
        {
            return FastRead(0);
        }

        public HidDeviceData FastRead(int timeout)
        {
            try
            {
                return ReadData(timeout);
            }
            catch
            {
                return new HidDeviceData(HidDeviceData.ReadStatus.ReadError);
            }
        }

        public void FastRead(ReadCallback callback)
        {
            FastRead(callback, 0);
        }

        public void FastRead(ReadCallback callback, int timeout)
        {
            var readDelegate = new ReadDelegate(FastRead);
            var asyncState = new HidAsyncState(readDelegate, callback);
            readDelegate.BeginInvoke(timeout, EndRead, asyncState);
        }

//        public async Task<HidDeviceData> FastReadAsync(int timeout = 0)
//        {
//            var readDelegate = new ReadDelegate(FastRead);
//#if NET20 || NET35
//            return await Task<HidDeviceData>.Factory.StartNew(() => readDelegate.Invoke(timeout));
//#else
//            return await Task<HidDeviceData>.Factory.FromAsync(readDelegate.BeginInvoke, readDelegate.EndInvoke, timeout, null);
//#endif
//        }

        public HidReport FastReadReport()
        {
            return FastReadReport(0);
        }

        public HidReport FastReadReport(int timeout)
        {
            return new HidReport(Capabilities.InputReportByteLength, FastRead(timeout));
        }

        public void FastReadReport(ReadReportCallback callback)
        {
            FastReadReport(callback, 0);
        }

        public void FastReadReport(ReadReportCallback callback, int timeout)
        {
            var readReportDelegate = new ReadReportDelegate(FastReadReport);
            var asyncState = new HidAsyncState(readReportDelegate, callback);
            readReportDelegate.BeginInvoke(timeout, EndReadReport, asyncState);
        }

//        public async Task<HidReport> FastReadReportAsync(int timeout = 0)
//        {
//            var readReportDelegate = new ReadReportDelegate(FastReadReport);
//#if NET20 || NET35
//            return await Task<HidReport>.Factory.StartNew(() => readReportDelegate.Invoke(timeout));
//#else
//            return await Task<HidReport>.Factory.FromAsync(readReportDelegate.BeginInvoke, readReportDelegate.EndInvoke, timeout, null);
//#endif
//        }
    }
    public class HidFastReadEnumerator : IHidEnumerator
    {
        public bool IsConnected(string devicePath)
        {
            return HidDevices.IsConnected(devicePath);
        }

        public IHidDevice GetDevice(string devicePath)
        {
            return Enumerate(devicePath).FirstOrDefault() as IHidDevice;
        }

        public IEnumerable<IHidDevice> Enumerate()
        {
            return HidDevices.EnumerateDevices().
                Select(d => new HidFastReadDevice(d.Path, d.Description) as IHidDevice);
        }

        public IEnumerable<IHidDevice> Enumerate(string devicePath)
        {
            return HidDevices.EnumerateDevices().Where(x => x.Path == devicePath).
                Select(d => new HidFastReadDevice(d.Path, d.Description) as IHidDevice);
        }

        public IEnumerable<IHidDevice> Enumerate(int vendorId, params int[] productIds)
        {
            return HidDevices.EnumerateDevices().Select(d => new HidFastReadDevice(d.Path, d.Description)).
                Where(f => f.Attributes.VendorId == vendorId && productIds.Contains(f.Attributes.ProductId)).
                Select(d => d as IHidDevice);
        }

        public IEnumerable<IHidDevice> Enumerate(int vendorId)
        {
            return HidDevices.EnumerateDevices().Select(d => new HidFastReadDevice(d.Path, d.Description)).
                Where(f => f.Attributes.VendorId == vendorId).
                Select(d => d as IHidDevice);
        }
    }
    public class HidReport
    {
        private byte _reportId;
        private byte[] _data = new byte[] { };

        private readonly HidDeviceData.ReadStatus _status;

        public HidReport(int reportSize)
        {
            Array.Resize(ref _data, reportSize - 1);
        }

        public HidReport(int reportSize, HidDeviceData deviceData)
        {
            _status = deviceData.Status;

            Array.Resize(ref _data, reportSize - 1);

            if ((deviceData.Data != null))
            {

                if (deviceData.Data.Length > 0)
                {
                    _reportId = deviceData.Data[0];
                    Exists = true;

                    if (deviceData.Data.Length > 1)
                    {
                        var dataLength = reportSize - 1;
                        if (deviceData.Data.Length < reportSize - 1) dataLength = deviceData.Data.Length;
                        Array.Copy(deviceData.Data, 1, _data, 0, dataLength);
                    }
                }
                else Exists = false;
            }
            else Exists = false;
        }

        public bool Exists { get; private set; }
        public HidDeviceData.ReadStatus ReadStatus { get { return _status; } }

        public byte ReportId
        {
            get { return _reportId; }
            set
            {
                _reportId = value;
                Exists = true;
            }
        }

        public byte[] Data
        {
            get { return _data; }
            set
            {
                _data = value;
                Exists = true;
            }
        }

        public byte[] GetBytes()
        {
            byte[] data = null;
            Array.Resize(ref data, _data.Length + 1);
            data[0] = _reportId;
            Array.Copy(_data, 0, data, 1, _data.Length);
            return data;
        }
    }
    public delegate void InsertedEventHandler();
    public delegate void RemovedEventHandler();

    public enum DeviceMode
    {
        NonOverlapped = 0,
        Overlapped = 1
    }

    [Flags]
    public enum ShareMode
    {
        Exclusive = 0,
        ShareRead = NativeMethods.FILE_SHARE_READ,
        ShareWrite = NativeMethods.FILE_SHARE_WRITE
    }

    public delegate void ReadCallback(HidDeviceData data);
    public delegate void ReadReportCallback(HidReport report);
    public delegate void WriteCallback(bool success);

    public interface IHidDevice : IDisposable
    {
        event InsertedEventHandler Inserted;
        event RemovedEventHandler Removed;

        IntPtr Handle { get; }
        bool IsOpen { get; }
        bool IsConnected { get; }
        string Description { get; }
        HidDeviceCapabilities Capabilities { get; }
        HidDeviceAttributes Attributes { get; }
        string DevicePath { get; }

        bool MonitorDeviceEvents { get; set; }

        void OpenDevice();

        void OpenDevice(DeviceMode readMode, DeviceMode writeMode, ShareMode shareMode);

        void CloseDevice();

        HidDeviceData Read();

        void Read(ReadCallback callback);

        void Read(ReadCallback callback, int timeout);

        Task<HidDeviceData> ReadAsync(int timeout = 0);

        HidDeviceData Read(int timeout);

        void ReadReport(ReadReportCallback callback);

        void ReadReport(ReadReportCallback callback, int timeout);

        Task<HidReport> ReadReportAsync(int timeout = 0);

        HidReport ReadReport(int timeout);
        HidReport ReadReport();

        bool ReadFeatureData(out byte[] data, byte reportId = 0);

        bool ReadProduct(out byte[] data);

        bool ReadManufacturer(out byte[] data);

        bool ReadSerialNumber(out byte[] data);

        void Write(byte[] data, WriteCallback callback);

        bool Write(byte[] data);

        bool Write(byte[] data, int timeout);

        void Write(byte[] data, WriteCallback callback, int timeout);

        Task<bool> WriteAsync(byte[] data, int timeout = 0);

        void WriteReport(HidReport report, WriteCallback callback);

        bool WriteReport(HidReport report);

        bool WriteReport(HidReport report, int timeout);

        void WriteReport(HidReport report, WriteCallback callback, int timeout);

        Task<bool> WriteReportAsync(HidReport report, int timeout = 0);

        HidReport CreateReport();

        bool WriteFeatureData(byte[] data);
    }
    public interface IHidEnumerator
    {
        bool IsConnected(string devicePath);
        IHidDevice GetDevice(string devicePath);
        IEnumerable<IHidDevice> Enumerate();
        IEnumerable<IHidDevice> Enumerate(string devicePath);
        IEnumerable<IHidDevice> Enumerate(int vendorId, params int[] productIds);
        IEnumerable<IHidDevice> Enumerate(int vendorId);
    }

    // Instance class that wraps HidDevices
    // The purpose of this is to allow consumer classes to create
    // their own enumeration abstractions, either for testing or
    // for comparing different implementations
    public class HidEnumerator : IHidEnumerator
    {
        public bool IsConnected(string devicePath)
        {
            return HidDevices.IsConnected(devicePath);
        }

        public IHidDevice GetDevice(string devicePath)
        {
            return HidDevices.GetDevice(devicePath) as IHidDevice;
        }

        public IEnumerable<IHidDevice> Enumerate()
        {
            return HidDevices.Enumerate().
                Select(d => d as IHidDevice);
        }

        public IEnumerable<IHidDevice> Enumerate(string devicePath)
        {
            return HidDevices.Enumerate(devicePath).
                Select(d => d as IHidDevice);
        }

        public IEnumerable<IHidDevice> Enumerate(int vendorId, params int[] productIds)
        {
            return HidDevices.Enumerate(vendorId, productIds).
                Select(d => d as IHidDevice);
        }

        public IEnumerable<IHidDevice> Enumerate(int vendorId)
        {
            return HidDevices.Enumerate(vendorId).
                Select(d => d as IHidDevice);
        }
    }
    internal static class NativeMethods
    {
        internal const int FILE_FLAG_OVERLAPPED = 0x40000000;
        internal const short FILE_SHARE_READ = 0x1;
        internal const short FILE_SHARE_WRITE = 0x2;
        internal const uint GENERIC_READ = 0x80000000;
        internal const uint GENERIC_WRITE = 0x40000000;
        internal const int ACCESS_NONE = 0;
        internal const int INVALID_HANDLE_VALUE = -1;
        internal const short OPEN_EXISTING = 3;
        internal const int WAIT_TIMEOUT = 0x102;
        internal const uint WAIT_OBJECT_0 = 0;
        internal const uint WAIT_FAILED = 0xffffffff;

        internal const int WAIT_INFINITE = -1;
        [StructLayout(LayoutKind.Sequential)]
        internal struct OVERLAPPED
        {
            public int Internal;
            public int InternalHigh;
            public int Offset;
            public int OffsetHigh;
            public int hEvent;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public IntPtr lpSecurityDescriptor;
            public bool bInheritHandle;
        }

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        static internal extern bool CancelIo(IntPtr hFile);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        static internal extern bool CancelIoEx(IntPtr hFile, IntPtr lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        static internal extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        static internal extern bool CancelSynchronousIo(IntPtr hObject);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        static internal extern IntPtr CreateEvent(ref SECURITY_ATTRIBUTES securityAttributes, int bManualReset, int bInitialState, string lpName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static internal extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, int dwShareMode, ref SECURITY_ATTRIBUTES lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, int hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        static internal extern bool ReadFile(IntPtr hFile, IntPtr lpBuffer, uint nNumberOfBytesToRead, out uint lpNumberOfBytesRead, [In] ref System.Threading.NativeOverlapped lpOverlapped);

        [DllImport("kernel32.dll")]
        static internal extern uint WaitForSingleObject(IntPtr hHandle, int dwMilliseconds);

        [DllImport("kernel32.dll", SetLastError = true)]
        static internal extern bool GetOverlappedResult(IntPtr hFile, [In] ref System.Threading.NativeOverlapped lpOverlapped, out uint lpNumberOfBytesTransferred, bool bWait);

        [DllImport("kernel32.dll")]
        static internal extern bool WriteFile(IntPtr hFile, byte[] lpBuffer, uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten, [In] ref System.Threading.NativeOverlapped lpOverlapped);

        internal const int DBT_DEVICEARRIVAL = 0x8000;
        internal const int DBT_DEVICEREMOVECOMPLETE = 0x8004;
        internal const int DBT_DEVTYP_DEVICEINTERFACE = 5;
        internal const int DBT_DEVTYP_HANDLE = 6;
        internal const int DEVICE_NOTIFY_ALL_INTERFACE_CLASSES = 4;
        internal const int DEVICE_NOTIFY_SERVICE_HANDLE = 1;
        internal const int DEVICE_NOTIFY_WINDOW_HANDLE = 0;
        internal const int WM_DEVICECHANGE = 0x219;
        internal const short DIGCF_PRESENT = 0x2;
        internal const short DIGCF_DEVICEINTERFACE = 0x10;
        internal const int DIGCF_ALLCLASSES = 0x4;

        internal const int MAX_DEV_LEN = 1000;
        internal const int SPDRP_ADDRESS = 0x1c;
        internal const int SPDRP_BUSNUMBER = 0x15;
        internal const int SPDRP_BUSTYPEGUID = 0x13;
        internal const int SPDRP_CAPABILITIES = 0xf;
        internal const int SPDRP_CHARACTERISTICS = 0x1b;
        internal const int SPDRP_CLASS = 7;
        internal const int SPDRP_CLASSGUID = 8;
        internal const int SPDRP_COMPATIBLEIDS = 2;
        internal const int SPDRP_CONFIGFLAGS = 0xa;
        internal const int SPDRP_DEVICE_POWER_DATA = 0x1e;
        internal const int SPDRP_DEVICEDESC = 0;
        internal const int SPDRP_DEVTYPE = 0x19;
        internal const int SPDRP_DRIVER = 9;
        internal const int SPDRP_ENUMERATOR_NAME = 0x16;
        internal const int SPDRP_EXCLUSIVE = 0x1a;
        internal const int SPDRP_FRIENDLYNAME = 0xc;
        internal const int SPDRP_HARDWAREID = 1;
        internal const int SPDRP_LEGACYBUSTYPE = 0x14;
        internal const int SPDRP_LOCATION_INFORMATION = 0xd;
        internal const int SPDRP_LOWERFILTERS = 0x12;
        internal const int SPDRP_MFG = 0xb;
        internal const int SPDRP_PHYSICAL_DEVICE_OBJECT_NAME = 0xe;
        internal const int SPDRP_REMOVAL_POLICY = 0x1f;
        internal const int SPDRP_REMOVAL_POLICY_HW_DEFAULT = 0x20;
        internal const int SPDRP_REMOVAL_POLICY_OVERRIDE = 0x21;
        internal const int SPDRP_SECURITY = 0x17;
        internal const int SPDRP_SECURITY_SDS = 0x18;
        internal const int SPDRP_SERVICE = 4;
        internal const int SPDRP_UI_NUMBER = 0x10;
        internal const int SPDRP_UI_NUMBER_DESC_FORMAT = 0x1d;

        internal const int SPDRP_UPPERFILTERS = 0x11;

        [StructLayout(LayoutKind.Sequential)]
        internal class DEV_BROADCAST_DEVICEINTERFACE
        {
            internal int dbcc_size;
            internal int dbcc_devicetype;
            internal int dbcc_reserved;
            internal Guid dbcc_classguid;
            internal short dbcc_name;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal class DEV_BROADCAST_DEVICEINTERFACE_1
        {
            internal int dbcc_size;
            internal int dbcc_devicetype;
            internal int dbcc_reserved;
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 16)]
            internal byte[] dbcc_classguid;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
            internal char[] dbcc_name;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class DEV_BROADCAST_HANDLE
        {
            internal int dbch_size;
            internal int dbch_devicetype;
            internal int dbch_reserved;
            internal int dbch_handle;
            internal int dbch_hdevnotify;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class DEV_BROADCAST_HDR
        {
            internal int dbch_size;
            internal int dbch_devicetype;
            internal int dbch_reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct SP_DEVICE_INTERFACE_DATA
        {
            internal int cbSize;
            internal System.Guid InterfaceClassGuid;
            internal int Flags;
            internal IntPtr Reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct SP_DEVINFO_DATA
        {
            internal int cbSize;
            internal Guid ClassGuid;
            internal int DevInst;
            internal IntPtr Reserved;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct SP_DEVICE_INTERFACE_DETAIL_DATA
        {
            internal int Size;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            internal string DevicePath;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct DEVPROPKEY
        {
            public Guid fmtid;
            public ulong pid;
        }

        internal static DEVPROPKEY DEVPKEY_Device_BusReportedDeviceDesc =
            new DEVPROPKEY { fmtid = new Guid(0x540b947e, 0x8b40, 0x45bc, 0xa8, 0xa2, 0x6a, 0x0b, 0x89, 0x4c, 0xbd, 0xa2), pid = 4 };

        [DllImport("setupapi.dll", EntryPoint = "SetupDiGetDeviceRegistryProperty")]
        public static extern bool SetupDiGetDeviceRegistryProperty(IntPtr deviceInfoSet, ref SP_DEVINFO_DATA deviceInfoData, int propertyVal, ref int propertyRegDataType, byte[] propertyBuffer, int propertyBufferSize, ref int requiredSize);

        [DllImport("setupapi.dll", EntryPoint = "SetupDiGetDevicePropertyW", SetLastError = true)]
        public static extern bool SetupDiGetDeviceProperty(IntPtr deviceInfo, ref SP_DEVINFO_DATA deviceInfoData, ref DEVPROPKEY propkey, ref ulong propertyDataType, byte[] propertyBuffer, int propertyBufferSize, ref int requiredSize, uint flags);

        [DllImport("setupapi.dll")]
        static internal extern bool SetupDiEnumDeviceInfo(IntPtr deviceInfoSet, int memberIndex, ref SP_DEVINFO_DATA deviceInfoData);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static internal extern IntPtr RegisterDeviceNotification(IntPtr hRecipient, IntPtr notificationFilter, Int32 flags);

        [DllImport("setupapi.dll")]
        internal static extern int SetupDiCreateDeviceInfoList(ref Guid classGuid, int hwndParent);

        [DllImport("setupapi.dll")]
        static internal extern int SetupDiDestroyDeviceInfoList(IntPtr deviceInfoSet);

        [DllImport("setupapi.dll")]
        static internal extern bool SetupDiEnumDeviceInterfaces(IntPtr deviceInfoSet, ref SP_DEVINFO_DATA deviceInfoData, ref Guid interfaceClassGuid, int memberIndex, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
        static internal extern IntPtr SetupDiGetClassDevs(ref System.Guid classGuid, string enumerator, int hwndParent, int flags);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, EntryPoint = "SetupDiGetDeviceInterfaceDetail")]
        static internal extern bool SetupDiGetDeviceInterfaceDetailBuffer(IntPtr deviceInfoSet, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData, IntPtr deviceInterfaceDetailData, int deviceInterfaceDetailDataSize, ref int requiredSize, IntPtr deviceInfoData);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
        static internal extern bool SetupDiGetDeviceInterfaceDetail(IntPtr deviceInfoSet, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData, ref SP_DEVICE_INTERFACE_DETAIL_DATA deviceInterfaceDetailData, int deviceInterfaceDetailDataSize, ref int requiredSize, IntPtr deviceInfoData);

        [DllImport("user32.dll")]
        static internal extern bool UnregisterDeviceNotification(IntPtr handle);

        internal const short HIDP_INPUT = 0;
        internal const short HIDP_OUTPUT = 1;

        internal const short HIDP_FEATURE = 2;
        [StructLayout(LayoutKind.Sequential)]
        internal struct HIDD_ATTRIBUTES
        {
            internal int Size;
            internal ushort VendorID;
            internal ushort ProductID;
            internal short VersionNumber;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct HIDP_CAPS
        {
            internal short Usage;
            internal short UsagePage;
            internal short InputReportByteLength;
            internal short OutputReportByteLength;
            internal short FeatureReportByteLength;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
            internal short[] Reserved;
            internal short NumberLinkCollectionNodes;
            internal short NumberInputButtonCaps;
            internal short NumberInputValueCaps;
            internal short NumberInputDataIndices;
            internal short NumberOutputButtonCaps;
            internal short NumberOutputValueCaps;
            internal short NumberOutputDataIndices;
            internal short NumberFeatureButtonCaps;
            internal short NumberFeatureValueCaps;
            internal short NumberFeatureDataIndices;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct HIDP_VALUE_CAPS
        {
            internal short UsagePage;
            internal byte ReportID;
            internal int IsAlias;
            internal short BitField;
            internal short LinkCollection;
            internal short LinkUsage;
            internal short LinkUsagePage;
            internal int IsRange;
            internal int IsStringRange;
            internal int IsDesignatorRange;
            internal int IsAbsolute;
            internal int HasNull;
            internal byte Reserved;
            internal short BitSize;
            internal short ReportCount;
            internal short Reserved2;
            internal short Reserved3;
            internal short Reserved4;
            internal short Reserved5;
            internal short Reserved6;
            internal int LogicalMin;
            internal int LogicalMax;
            internal int PhysicalMin;
            internal int PhysicalMax;
            internal short UsageMin;
            internal short UsageMax;
            internal short StringMin;
            internal short StringMax;
            internal short DesignatorMin;
            internal short DesignatorMax;
            internal short DataIndexMin;
            internal short DataIndexMax;
        }

        [DllImport("hid.dll")]
        static internal extern bool HidD_FlushQueue(IntPtr hidDeviceObject);

        [DllImport("hid.dll")]
        static internal extern bool HidD_GetAttributes(IntPtr hidDeviceObject, ref HIDD_ATTRIBUTES attributes);

        [DllImport("hid.dll")]
        static internal extern bool HidD_GetFeature(IntPtr hidDeviceObject, byte[] lpReportBuffer, int reportBufferLength);

        [DllImport("hid.dll")]
        static internal extern bool HidD_GetInputReport(IntPtr hidDeviceObject, byte[] lpReportBuffer, int reportBufferLength);

        [DllImport("hid.dll")]
        static internal extern void HidD_GetHidGuid(ref Guid hidGuid);

        [DllImport("hid.dll")]
        static internal extern bool HidD_GetNumInputBuffers(IntPtr hidDeviceObject, ref int numberBuffers);

        [DllImport("hid.dll")]
        static internal extern bool HidD_GetPreparsedData(IntPtr hidDeviceObject, ref IntPtr preparsedData);

        [DllImport("hid.dll")]
        static internal extern bool HidD_FreePreparsedData(IntPtr preparsedData);

        [DllImport("hid.dll")]
        static internal extern bool HidD_SetFeature(IntPtr hidDeviceObject, byte[] lpReportBuffer, int reportBufferLength);

        [DllImport("hid.dll")]
        static internal extern bool HidD_SetNumInputBuffers(IntPtr hidDeviceObject, int numberBuffers);

        [DllImport("hid.dll")]
        static internal extern bool HidD_SetOutputReport(IntPtr hidDeviceObject, byte[] lpReportBuffer, int reportBufferLength);

        [DllImport("hid.dll")]
        static internal extern int HidP_GetCaps(IntPtr preparsedData, ref HIDP_CAPS capabilities);

        [DllImport("hid.dll")]
        static internal extern int HidP_GetValueCaps(short reportType, ref byte valueCaps, ref short valueCapsLength, IntPtr preparsedData);

        [DllImport("hid.dll", CharSet = CharSet.Unicode)]
        internal static extern bool HidD_GetProductString(IntPtr hidDeviceObject, ref byte lpReportBuffer, int ReportBufferLength);

        [DllImport("hid.dll", CharSet = CharSet.Unicode)]
        internal static extern bool HidD_GetManufacturerString(IntPtr hidDeviceObject, ref byte lpReportBuffer, int ReportBufferLength);

        [DllImport("hid.dll", CharSet = CharSet.Unicode)]
        internal static extern bool HidD_GetSerialNumberString(IntPtr hidDeviceObject, ref byte lpReportBuffer, int reportBufferLength);
    }
}

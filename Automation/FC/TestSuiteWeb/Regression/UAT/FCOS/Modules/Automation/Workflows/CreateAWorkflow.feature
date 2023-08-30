Feature: CreateAWorkflow

Multiple conditions and multiple actions to be covered - TO DO

@regression
Scenario: Create different variations of workflows
	Given The website FcosAzure is started
	When Login to FcosAzure as broker.testim@franchiczar.com, FranchiCzar!
	Then Go to Automation Workflows
		And Click Create
	#Then Select FranchiCzar as a workflow company
	Then Select first option from Workflow company dropdown
	And Set the workflow to Trigger When <trigger>
	And Add an action <action>
	##Then Delete Action
	Then Save Workflow <example description>
	##Then Save Draft Workflow
	##Then Deactivate Workflow
	#Then Verify Workflow
Examples:
	| example description                             | trigger                        | action                 |
	| Activity Added Add a note                       | an activity is added           | Add a note             |
	| Activity Added Add a tag                        | an activity is added           | Add a tag              |
	| Activity Added Assign to a user                 | an activity is added           | Assign to a user       |
	| Activity Added Create a new ticket              | an activity is added           | Create a new ticket    |
	| Activity Added Send an email                    | an activity is added           | Send an email          |
	| Activity Added Send FCOS notification           | an activity is added           | Send FCOS notification |
	| Activity Added Send a text message              | an activity is added           | Send a text message    |
	| Activity Added Send a webhook                   | an activity is added           | Send a webhook         |
	| Activity Added Start an outgoing call           | an activity is added           | Start an outgoing call |
	| Activity Added Update a field                   | an activity is added           | Update a field         |

	| Email Opened Add a note                         | an email is opened             | Add a note             |
	| Email Opened Add a tag                          | an email is opened             | Add a tag              |
	| Email Opened Assign to a user                   | an email is opened             | Assign to a user       |
	| Email Opened Create a new ticket                | an email is opened             | Create a new ticket    |
	| Email Opened Send an email                      | an email is opened             | Send an email          |
	| Email Opened Send FCOS notification             | an email is opened             | Send FCOS notification |
	| Email Opened Send a text message                | an email is opened             | Send a text message    |
	| Email Opened Send a webhook                     | an email is opened             | Send a webhook         |
	| Email Opened Start an outgoing call             | an email is opened             | Start an outgoing call |
	| Email Opened Update a field                     | an email is opened             | Update a field         |

	| Form Submitted Send an email                    | a form is submitted            | Send an email          |
	| Form submission requested Send an email         | a form submission is requested | Send an email          |

	| Customer Has ReInquiry Add a note               | a customer has a re-inquiry    | Add a note             |
	| Customer Has ReInquiry Add a tag                | a customer has a re-inquiry    | Add a tag              |
	| Customer Has ReInquiry Assign to a user         | a customer has a re-inquiry    | Assign to a user       |
	| Customer Has ReInquiry Create a new ticket      | a customer has a re-inquiry    | Create a new ticket    |
	| Customer Has ReInquiry Send an email            | a customer has a re-inquiry    | Send an email          |
	| Customer Has ReInquiry Send FCOS notification   | a customer has a re-inquiry    | Send FCOS notification |
	| Customer Has ReInquiry Send a text message      | a customer has a re-inquiry    | Send a text message    |
	| Customer Has ReInquiry Send a webhook           | a customer has a re-inquiry    | Send a webhook         |
	| Customer Has ReInquiry Start an outgoing call   | a customer has a re-inquiry    | Start an outgoing call |
	| Customer Has ReInquiry Update a field           | a customer has a re-inquiry    | Update a field         |

	| Lead Created Add a note                         | a lead is created              | Add a note             |
	| Lead Created Add a tag                          | a lead is created              | Add a tag              |
	| Lead Created Assign to a user                   | a lead is created              | Assign to a user       |
	| Lead Created Create a new ticket                | a lead is created              | Create a new ticket    |
	| Lead Created Send an email                      | a lead is created              | Send an email          |
	| Lead Created Send FCOS notification             | a lead is created              | Send FCOS notification |
	| Lead Created Send a text message                | a lead is created              | Send a text message    |
	| Lead Created Send a webhook                     | a lead is created              | Send a webhook         |
	| Lead Created Start an outgoing call             | a lead is created              | Start an outgoing call |
	| Lead Created Update a field                     | a lead is created              | Update a field         |

	| Lead Or Node Created Add a note                 | a lead or node is created      | Add a note             |
	| Lead Or Node Created Add a tag                  | a lead or node is created      | Add a tag              |
	| Lead Or Node Created Assign to a user           | a lead or node is created      | Assign to a user       |
	| Lead Or Node Created Create a new ticket        | a lead or node is created      | Create a new ticket    |
	| Lead Or Node Created Send an email              | a lead or node is created      | Send an email          |
	| Lead Or Node Created Send FCOS notification     | a lead or node is created      | Send FCOS notification |
	| Lead Or Node Created Send a text message        | a lead or node is created      | Send a text message    |
	| Lead Or Node Created Send a webhook             | a lead or node is created      | Send a webhook         |
	| Lead Or Node Created Start an outgoing call     | a lead or node is created      | Start an outgoing call |
	| Lead Or Node Created Update a field             | a lead or node is created      | Update a field         |

	| Lead Or Customer Created Add a note             | a lead or customer is created  | Add a note             |
	| Lead Or Customer Created Add a tag              | a lead or customer is created  | Add a tag              |
	| Lead Or Customer Created Assign to a user       | a lead or customer is created  | Assign to a user       |
	| Lead Or Customer Created Create a new ticket    | a lead or customer is created  | Create a new ticket    |
	| Lead Or Customer Created Send an email          | a lead or customer is created  | Send an email          |
	| Lead Or Customer Created Send FCOS notification | a lead or customer is created  | Send FCOS notification |
	| Lead Or Customer Created Send a text message    | a lead or customer is created  | Send a text message    |
	| Lead Or Customer Created Send a webhook         | a lead or customer is created  | Send a webhook         |
	| Lead Or Customer Created Start an outgoing call | a lead or customer is created  | Start an outgoing call |
	| Lead Or Customer Created Update a field         | a lead or customer is created  | Update a field         |

	| Lead Or Node Modified Add a note                | a lead or node is modified     | Add a note             |
	| Lead Or Node Modified Add a tag                 | a lead or node is modified     | Add a tag              |
	| Lead Or Node Modified Assign to a user          | a lead or node is modified     | Assign to a user       |
	| Lead Or Node Modified Create a new ticket       | a lead or node is modified     | Create a new ticket    |
	| Lead Or Node Modified Send an email             | a lead or node is modified     | Send an email          |
	| Lead Or Node Modified Send FCOS notification    | a lead or node is modified     | Send FCOS notification |
	| Lead Or Node Modified Send a text message       | a lead or node is modified     | Send a text message    |
	| Lead Or Node Modified Send a webhook            | a lead or node is modified     | Send a webhook         |
	| Lead Or Node Modified Start an outgoing call    | a lead or node is modified     | Start an outgoing call |
	| Lead Or Node Modified Update a field            | a lead or node is modified     | Update a field         |

	| Node Has A ReInquiry Add a note                 | a node has a re-inquiry        | Add a note             |
	| Node Has A ReInquiry Add a tag                  | a node has a re-inquiry        | Add a tag              |
	| Node Has A ReInquiry Assign to a user           | a node has a re-inquiry        | Assign to a user       |
	| Node Has A ReInquiry Create a new ticket        | a node has a re-inquiry        | Create a new ticket    |
	| Node Has A ReInquiry Send an email              | a node has a re-inquiry        | Send an email          |
	| Node Has A ReInquiry Send FCOS notification     | a node has a re-inquiry        | Send FCOS notification |
	| Node Has A ReInquiry Send a text message        | a node has a re-inquiry        | Send a text message    |
	| Node Has A ReInquiry Send a webhook             | a node has a re-inquiry        | Send a webhook         |
	| Node Has A ReInquiry Start an outgoing call     | a node has a re-inquiry        | Start an outgoing call |
	| Node Has A ReInquiry Update a field             | a node has a re-inquiry        | Update a field         |

	| Note Added Add a tag                            | a note is added                | Add a tag              |
	| Note Added Assign to a user                     | a note is added                | Assign to a user       |
	| Note Added Create a new ticket                  | a note is added                | Create a new ticket    |
	| Note Added Send an email                        | a note is added                | Send an email          |
	| Note Added Send FCOS notification               | a note is added                | Send FCOS notification |
	| Note Added Send a text message                  | a note is added                | Send a text message    |
	| Note Added Send a webhook                       | a note is added                | Send a webhook         |
	| Note Added Start an outgoing call               | a note is added                | Start an outgoing call |
	| Note Added Update a field                       | a note is added                | Update a field         |

	| Purchase Order Paid Send an email               | a purchase order is paid       | Send an email          |
	| Purchase Order Paid Send a text message         | a purchase order is paid       | Send a text message    |

	| Task Action Taken Add a note                    | a task action is taken         | Add a note             |
	| Task Action Taken Add a tag                     | a task action is taken         | Add a tag              |
	| Task Action Taken Assign to a user              | a task action is taken         | Assign to a user       |
	| Task Action Taken Create a new ticket           | a task action is taken         | Create a new ticket    |
	| Task Action Taken Send an email                 | a task action is taken         | Send an email          |
	| Task Action Taken Send FCOS notification        | a task action is taken         | Send FCOS notification |
	| Task Action Taken Send a text message           | a task action is taken         | Send a text message    |
	| Task Action Taken Send a webhook                | a task action is taken         | Send a webhook         |
	| Task Action Taken Start an outgoing call        | a task action is taken         | Start an outgoing call |
	| Task Action Taken Update a field                | a task action is taken         | Update a field         |

	| Task Has Expired Add a note                     | a task has expired             | Add a note             |
	| Task Has Expired Add a tag                      | a task has expired             | Add a tag              |
	| Task Has Expired Assign to a user               | a task has expired             | Assign to a user       |
	| Task Has Expired Create a new ticket            | a task has expired             | Create a new ticket    |
	| Task Has Expired Send an email                  | a task has expired             | Send an email          |
	| Task Has Expired Send FCOS notification         | a task has expired             | Send FCOS notification |
	| Task Has Expired Send a text message            | a task has expired             | Send a text message    |
	| Task Has Expired Send a webhook                 | a task has expired             | Send a webhook         |
	| Task Has Expired Start an outgoing call         | a task has expired             | Start an outgoing call |
	| Task Has Expired Update a field                 | a task has expired             | Update a field         |

	| Task Activated Add a note                       | a task is activated            | Add a note             |
	| Task Activated Add a tag                        | a task is activated            | Add a tag              |
	| Task Activated Assign to a user                 | a task is activated            | Assign to a user       |
	| Task Activated Create a new ticket              | a task is activated            | Create a new ticket    |
	| Task Activated Send an email                    | a task is activated            | Send an email          |
	| Task Activated Send FCOS notification           | a task is activated            | Send FCOS notification |
	| Task Activated Send a text message              | a task is activated            | Send a text message    |
	| Task Activated Send a webhook                   | a task is activated            | Send a webhook         |
	| Task Activated Start an outgoing call           | a task is activated            | Start an outgoing call |
	| Task Activated Update a field                   | a task is activated            | Update a field         |

	| Task Completed Add a note                       | a task is completed            | Add a note             |
	| Task Completed Add a tag                        | a task is completed            | Add a tag              |
	| Task Completed Assign to a user                 | a task is completed            | Assign to a user       |
	| Task Completed Create a new ticket              | a task is completed            | Create a new ticket    |
	| Task Completed Send an email                    | a task is completed            | Send an email          |
	| Task Completed Send FCOS notification           | a task is completed            | Send FCOS notification |
	| Task Completed Send a text message              | a task is completed            | Send a text message    |
	| Task Completed Send a webhook                   | a task is completed            | Send a webhook         |
	| Task Completed Start an outgoing call           | a task is completed            | Start an outgoing call |
	| Task Completed Update a field                   | a task is completed            | Update a field         |

	| Ticket Created Add a note                       | a ticket is created            | Add a note             |
	| Ticket Created Add a tag                        | a ticket is created            | Add a tag              |
	| Ticket Created Assign to a user                 | a ticket is created            | Assign to a user       |
	| Ticket Created Send an email                    | a ticket is created            | Send an email          |
	| Ticket Created Send FCOS notification           | a ticket is created            | Send FCOS notification |
	| Ticket Created Send a text message              | a ticket is created            | Send a text message    |
	| Ticket Created Send a webhook                   | a ticket is created            | Send a webhook         |
	| Ticket Created Start an outgoing call           | a ticket is created            | Start an outgoing call |

	| Ticket Modified Add a note                      | a ticket is modified           | Add a note             |
	| Ticket Modified Add a tag                       | a ticket is modified           | Add a tag              |
	| Ticket Modified Assign to a user                | a ticket is modified           | Assign to a user       |
	| Ticket Modified Send an email                   | a ticket is modified           | Send an email          |
	| Ticket Modified Send FCOS notification          | a ticket is modified           | Send FCOS notification |
	| Ticket Modified Send a text message             | a ticket is modified           | Send a text message    |
	| Ticket Modified Send a webhook                  | a ticket is modified           | Send a webhook         |
	| Ticket Modified Start an outgoing call          | a ticket is modified           | Start an outgoing call |

	| Ticket Detail Created Add a note                | a ticket detail is created     | Add a note             |
	| Ticket Detail Created Add a tag                 | a ticket detail is created     | Add a tag              |
	| Ticket Detail Created Assign to a user          | a ticket detail is created     | Assign to a user       |
	| Ticket Detail Created Send an email             | a ticket detail is created     | Send an email          |
	| Ticket Detail Created Send FCOS notification    | a ticket detail is created     | Send FCOS notification |
	| Ticket Detail Created Send a text message       | a ticket detail is created     | Send a text message    |
	| Ticket Detail Created Send a webhook            | a ticket detail is created     | Send a webhook         |
	| Ticket Detail Created Start an outgoing call    | a ticket detail is created     | Start an outgoing call |

	| Web Request Received Add a note                 | a web request is received      | Add a note             |
	| Web Request Received Add a tag                  | a web request is received      | Add a tag              |
	| Web Request Received Assign to a user           | a web request is received      | Assign to a user       |
	| Web Request Received Send an email              | a web request is received      | Send an email          |
	| Web Request Received Send FCOS notification     | a web request is received      | Send FCOS notification |
	| Web Request Received Send a text message        | a web request is received      | Send a text message    |
	| Web Request Received Send a webhook             | a web request is received      | Send a webhook         |
	| Web Request Received Start an outgoing call     | a web request is received      | Start an outgoing call |
	| Web Request Received Update a field             | a web request is received      | Update a field         |



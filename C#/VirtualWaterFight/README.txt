RELIABLE COMMUNICATION
Four classes are designed to handle reliable communications. They are in the Common Project under the folder of Threading. PlayerConversation and PlayerConversationList classes are implemented for Player. Similarly, ManagerConversation and ManagerConversationList classes are for Fight Manager, Balloon Manager and Water Manager. 
Player and managers create conversations for some the protocols with using PlayerConversation and ManagerConversation respectively.  They all create lists to keep track of conversations. PlayerConversationList and ManagerConversationList are designed for this purpose.
There are some functions in PlayerDoer, FightManagerDoer, BalloonManager and WaterFightManager for handle message lost and duplicate messages (i.e. isRequestValid(), isReplyValid()).

HOW TO RUN THE PROGRAM
Since there’s no GUI, just run test cases collected in ‘TestVirtualWaterFight’ project.

WHERE ARE TEST CASES
There are some test classes in ‘TestVirtualWaterFight’ project, just run each class.

ADVANCED REQUIREMENTS
I tried to address ‘Additional using test cases’ and ‘Complete 100% of your system’s requirements'. 
‘Additional using test cases’: There was only one test case for Communicator class in HW 4 but hopefully I could make many additional test cases for conversation, communication protocols and communications with WFSS web server.
‘Complete 100% of your system’s requirements': I’ve implemented all communication protocols and made some changes in protocols and messages.
All communication protocols and resource management are implemented. Player, Fight Manager, Balloon Manager and Water Manager have specified doers for each protocol, put in folders named ‘Protocol Doers’ in their own project. 
Large variety of test cases provided in the project named ‘TestVirtualWaterFight’. There is a test class for each protocol. Test classes include test methods for each doer of a protocol. More precisely all protocols have a doer in each end to handle the process. For example, Player and Fight Manager have a distinct doer for registration protocol, RegistrationRequestDoer for Player and RegistrationReplyDoer for Fight Manager. 


Laleh Rostami Hosoori A017772483

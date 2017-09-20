<a href="http://buildserver.spawtz.com:8000/viewType.html?buildTypeId=PseudoCQRS_Ci&guest=1">
<img src="http://buildserver.spawtz.com:8000/app/rest/builds/buildType:(id:PseudoCQRS_Ci)/statusIcon"/>
</a>


# PseudoCQRS

Pseudo CQRS is a asp.net mvc based framework to create web applications using the CQRS pattern (http://martinfowler.com/bliki/CQRS.html). PseudoCQRS is not a true CQRS implementation, hence the name PseudoCQRS. PseudoCQRS can be used in an existing mvc project as it does not require you to make any changes to your persistence mechanism, as it does not require you to use event sourcing.

The main concept of CQRS is that you should have a query side which displays data and a command side which manipulates data.  In PseudoCQRS you create ViewModelProviders which return a ViewModels (query side) and Commands handled by command handlers which make modifications to the domain (command side). 

The CQRS pattern also describes an event notification system whereby you can create event subscribers which subscribe to events that are raised by the CommandHandlers.  These event subscribers act asyncronously to do things like send email notifications, record change history, update read models etc.  This pattern is implemented in PseudoCQRS and help.

For a detailed description of the usage of PseudoCQRS, please see the wiki.  There is also a sample application included in the source.

Give it a try and shout if you need help.

# Packaging
To create the NuGet packages run CreatePackages.ps1
PseudoCQRS
==========

Pseudo CQRS is a asp.net mvc based framework to create web applications using CQRS framework. PseudoCQRS is not a true CQRS implementation which is also a reason to name it PseudoCQRS. PseudoCQRS can be used in a new or any existing mvc project as it do not expect you to make any changes in db because it do not support event sourcing.

The concept of CQRS is that you should have a read side which displays data and a write side which manipulates data therefore in PseudoCQRS you create a ViewModelProvider which returns a ViewModel (read side) and create a CommandHandler which accepts a Command which contains information modified by user (write side). 

CQRS pattern also describe an event notification system which is basically when you execute a command then you may want to notify some other objects, so you can create 0 or more event subscribers which are subscribed to that event and when you raise that event then those event subscribers will be notified and they will be called. This feature is also available in PseudoCQRS and it will help you to increase SRP (Single Responsibility Principle) in your project.

Beside these CQRS related features, PseudoCQRS supports checkers which are basically three different ways to apply checks on your read and write data models. 

1) You can have validation checkers which you can apply on your commands to validate if you should execute you command.

2) Authorization checkers, which can be applied on read and write side models to check if user is authorized to view the page or make changes

3) Access checkers, can be applied on both read and write side models to check if user has access to a specific item.

Please note that PseudoCQRS do not have any internal validation engine therefore you are free to use whatever validation framework with which you are familiar.

Give it a try and shout if you need help.

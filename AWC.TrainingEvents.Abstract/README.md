# Abstract 
## `AWC.TraininingEvents.Abstract`

The abstract project sets up the blueprint for the whole application including the domain and supporting domain model contracts, service contracts, and repository contracts.

With SOLID and DDD patterns we generally include the interfaces in the most stable project (in this case it would be the ActivityService); however, I like keeping an abstract project and the ActivityService project is only allowed to reference this project so that it still remains the (second) most stable.

## [`IModels`][IModels]

This is something that I do uniquely, but I find it is very useful. It's essentially a blue-print for what the model implementation should look like in the various layers--the common props that would carry across the data, service/domain, and client layer. 

[more...][IModels]

## IServices
Services coordinate the word that needs to be done before and after interacting with our Domain Entity. For us, it means interacting with the data layer and the domain entity. For more complex situations it can include interacting with other services, framework adapters, etc. 

## IRepositories

I like to think of repositories as just another service. But since the data access layer is such a common service, I keep it seperate from services themselves.

[IModels]: \IModels\README.md


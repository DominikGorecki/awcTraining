# AWC Training Events Signup
Simple registration app published on [azure][live]. 

Useful Live URLs:

* [Live Website][live]
* [Live Swagger API Docs][swaggerLive]
* [GitHub Repo][repo]

## Back end

Technologies Used

* .Net Core 
* Entity Framework Core
* Unit Tests
* Automapper
* Swashbuckle
* MsTest
* Moq

### Architecture

* SOLID Programming Patterns
* Domain Driven Design (light)
* Services and Repo are in their own modules (projects) so we can support independent deployments or seperating out completely in the future -- most modular
* Can easily convert to micro-service implementation

#### Abstract [`AWC.TraininingEvents.Abstract`][abstractREADME]

The abstract project sets up the blueprint for the whole application including the domain and supporting domain model contracts, service contracts, and repository contracts.

[more...][abstractREADME]

#### ActivityService [`AWC.TrainingEvents.ActivityService`][activityServiceREADME]

The main service that implements the main domain model that implements the interface `IActivitySignup` where all tbe business logic lives and most of the unit tests focus on.

[more...][activityServiceREADME]

#### Data [`AWC.TrainingEvents.Data`][dataREADME]

The EF Core implementatiopn with the main ApplicationDbContext that can be shared across multiple Data services but for now are only used by `ActivityData` that implements `IActivityData`. By doing this, we centralize our data service implementations, which breaks with micro-service like implementations, which are nicely scalable, but takes advantage of EF Core features for caching since we use the "Transient" DI (it's possible that for one call only one ApplicationDbContext is instantiated and run across multiple Data services). However, we can easily break away from this pattern by implementing a applicationDbContext in multiple projects if we want to scale data services independently of each other. 

[more...][dataREADME]

#### UnitTests `AWC.Tests`

Simple MS Test implementation with a couple of factories (factory pattern) for generating commonly used mock implementations for models, services, and responses.

#### API and SPA Serving `AWC.TrainingEventsWeb`

Used the scaffold that generates the SPA implementation, but I completely over-wrote the reactJS code and implementing my own controllers for the API.




## Front End

Technologies Used:

* Bootstrap (react-bootstrap)
* ReactJS
* react-bootstrap-table-next (table2)


Form Build with:
* Formik
* yup (validation schemas) 
* react-select (activities drop down)
* react-textarea-autosize (comments)
* react-datepicker

## Settings
SQL Server settings for debuggin use local and integrated security--it's safe because there are no passwords in the repo. In production, they are over-ridden by the application service setup in azure.

[abstractREADME]:\AWC.TrainingEvents.Abstract\README.md
[activityServiceREADME]:\AWC.TrainingEvents.ActivityService\README.md
[dataREADME]:\AWC.TrainingEvents.Data\README.md
[live]:https://awctrainingeventsweb.azurewebsites.net/
[swaggerLive]:https://awctrainingeventsweb.azurewebsites.net/swagger/index.html
[repo]: https://github.com/DominikGorecki/awcTraining
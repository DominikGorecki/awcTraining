## IModels

I like abstracting my main domain models with interfaces since C# supports `properties` in interfaces. The other options is to use an abstract class to ensure maximal stability and we're abiding by the stable abstraction principle (R. Martin 128).

This is especially useful if we need to use the same model blueprint across module boundaries such as between the service and architecture layer. 

*Note* that the model iterface specify that only a getter is required. The addition of setter is up to the module that implements the interface.

In this way, we will keep our solution very modular without sacrificing too much extra implementation time overhead (maybe an extra 10% more in the beginning, with huge cost savings in the back-end when we do future development).

### IActivity

In this case, we'll be using `IActivity` because it will be used in `ActivityService` and `TrainingEventsData` modules (projects).

### ILogin

Another model that could be abstract when we are ready to add authentication/autherization is for Login.

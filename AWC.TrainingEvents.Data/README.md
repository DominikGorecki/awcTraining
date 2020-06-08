# `AWC.TrainingEvents.Data`

The EF Core implementatiopn with the main ApplicationDbContext that can be shared across multiple Data services but for now are only used by `ActivityData` that implements `IActivityData`. By doing this, we centralize our data service implementations, which breaks with micro-service like implementations, which are nicely scalable, but takes advantage of EF Core features for caching since we use the "Transient" DI (it's possible that for one call only one ApplicationDbContext is instantiated and run across multiple Data services). However, we can easily break away from this pattern by implementing a applicationDbContext in multiple projects if we want to scale data services independently of each other. 



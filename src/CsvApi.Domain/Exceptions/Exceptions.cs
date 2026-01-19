namespace CsvApi.Domain;

public class CannotBeBeforeException : Exception
{
    public CannotBeBeforeException()
        : base("Start date cannot be before 01.01.2000") { }
}

public class CannotBeFutureException : Exception
{
    public CannotBeFutureException() 
        : base("Start date cannot be in the future") { }
}

public class ValueCannotBeNegativeException : Exception
{
    public ValueCannotBeNegativeException() 
        : base("Value cannot be negative") { }
}

public class ExecutionTimeCannotBeNegativeException : Exception
{
    public ExecutionTimeCannotBeNegativeException() 
        : base("Execution time cannot be negative") { }
}

public class ToManyOperationsException : Exception
{
    public ToManyOperationsException() 
        : base("Operations count cannot be more then 10000") { }
}

public class NoOperationsException : Exception
{
    public NoOperationsException() 
        : base("Operations count cannot be 0") { }
}

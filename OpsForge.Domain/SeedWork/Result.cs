namespace OpsForge.Domain.SeedWork;

public record Result
{
    protected Result(bool isSuccess, string error)
    {
        if (isSuccess && !string.IsNullOrWhiteSpace(error))
            throw new InvalidOperationException();
        if (!isSuccess && string.IsNullOrWhiteSpace(error))
            throw new InvalidOperationException();

        this.IsSuccess = isSuccess;
        this.Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !this.IsSuccess;
    public string Error { get; }

    public static Result Success() 
        => new Result(true, string.Empty);
    public static Result Failure(string error)
        => new Result(false, error);
}

public record Result<T> : Result
{
    private readonly T _value;

    protected Result(T value, bool isSuccess, string error) 
        : base(isSuccess, error)
    {
        this._value = value;
    }

    public T Value
    {
        get
        {
            if (IsFailure)
                throw new InvalidOperationException("Unable to obtain value for failed result.");
            return _value;
        }
    }

    public static Result<T> Success(T value)
        => new Result<T>(value, true, string.Empty);
    public new static Result<T> Failure(string error)
        => new Result<T>(default, false, error);
}

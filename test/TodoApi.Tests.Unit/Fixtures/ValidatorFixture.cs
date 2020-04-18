namespace TodoApi.Tests.Unit.Fixtures
{
    public class ValidatorFixture<T> where T : class, new()
    {
        public ValidatorFixture()
        {
            Instance = new T();
        }
        
        public T Instance { get; }
    }
}
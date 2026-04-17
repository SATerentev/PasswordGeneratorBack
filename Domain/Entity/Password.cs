namespace PassGeneratorService.Domain.Entity
{
    public class Password
    {
        public Guid Id { get; private set; }
        public string Value { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Password() { }

        public Password(string pass)
        {
            if (string.IsNullOrWhiteSpace(pass))
                throw new ArgumentException("Пароль не может быть пустым.", nameof(pass));

            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Value = pass;
        }
    }
}

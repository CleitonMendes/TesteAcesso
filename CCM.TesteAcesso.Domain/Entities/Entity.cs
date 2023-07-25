namespace CCM.TesteAcesso.Domain.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            CreatedDate = DateTime.UtcNow;
        }

        public long Id { get; protected set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public void SetUpdateDate() => UpdatedDate = DateTime.Now;
    }
}

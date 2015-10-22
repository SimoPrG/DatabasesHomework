namespace EntityFramework
{
    using System.Data.Linq;

    // 8.
    // By inheriting the Employee entity class create a class which allows employees to access their corresponding
    // territories as property of type EntitySet<T>.
    public partial class Employee
    {
        public EntitySet<Territory> CorrespondingTerritories
        {
            get
            {
                var entitySet = new EntitySet<Territory>();
                entitySet.AddRange(Territories);
                return entitySet;
            }
        }
    }
}

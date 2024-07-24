
namespace Sente.Domain.Models
{
    public class WorklogAnalysisResult
    {
        public string Author { get; set; }
        public List<string> Authors
        {
            get
            {
                return AllIndyvidualHoursWithTypes.Keys.ToList();
            }
        }

        public List<string> Types
        {
            get
            {
                var result = new List<string>();

                foreach (var t in AllHoursWithTypes)
                {
                    result.Add(t.Qualification.ItemSymbol);
                }

                return result.Distinct().ToList();
            }
        }

        public List<Qualification> Qualifications => AllHoursWithTypes.Select(h => h.Qualification).Distinct().ToList();


        public List<Hours> AllHoursWithTypes { get; set; } = new();
        public Dictionary<string, List<Hours>> AllIndyvidualHoursWithTypes { get; set; } = new();

    }
    public class Hours(Qualification qualification, double value)
    {
        public Qualification Qualification { get; } = qualification;
        public double Value { get; } = value;
    }

    public class Qualification(string name, string itemSymbol)
    {
        public string Name { get; } = name;
        public string ItemSymbol { get; } = itemSymbol;
    }
}

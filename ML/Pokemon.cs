namespace ML
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Sprites Sprites { get; set; }
        public List<object> Pokemons { get; set; }
        public stats stats { get; set; }
        public types types { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public string url { get; set; }
    }
    public class Sprites
    {
        public string front_default { get; set; }
        public string front_shiny { get; set; }
    }
    public class stats
    {
        public int base_stat { get; set; }
        public int effort { get; set; }
        public string name { get; set; }
        public List<object> Stats { get; set; }
        public Stat Stat { get; set; }
    }
    public class Stat
    {
        public string name { get; set; }
    }
    public class types
    {
        public int  slot { get; set; }
        public type type { get; set; }
        public string name { get; set; }
        public List<object> Types { get; set; }
    }
    public class type
    {
        public string name { get; set; }
    }
}
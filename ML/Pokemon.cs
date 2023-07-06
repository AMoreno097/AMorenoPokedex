﻿namespace ML
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Sprites Sprites { get; set; }
        public List<object> stats { get; set; }
        public List<object> types { get; set; }
        public List<object> Pokemons { get; set; }
    }
    public class Sprites
    {
        public string front_default { get; set; }
        public string front_shiny { get; set; }
    }
    public class Stats
    {
        public int base_stat { get; set; }
        public int effort { get; set; }
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
    }
    public class type
    {
        public string name { get; set; }
    }
}
namespace WorkdayCalendarLib
{
    public abstract class Holiday {

        public string Name { get; private set;}

        public abstract bool Match(DateTime dt);

        protected Holiday(string name = "") {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
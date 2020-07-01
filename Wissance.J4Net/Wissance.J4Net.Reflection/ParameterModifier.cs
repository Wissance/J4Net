namespace Wissance.J4Net.Reflection
{
    public class ParameterModifier
    {
        public ParameterModifier(int parameterCount)
        {
            values = new bool[parameterCount];
        }

        public bool this[int index]
        {
            get { return values[index]; }
            set { values[index] = value; }
        }
        
        private readonly bool[] values;
    }
}
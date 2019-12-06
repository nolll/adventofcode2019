using System.Collections.Generic;

namespace Core.Computer.Instructions
{
    public class EqualsInstruction : Instruction
    {
        public override InstructionType Type => InstructionType.Equals;

        public EqualsInstruction(int[] memory, int position, IList<ParameterType> parameterTypes)
            : base(memory, position, parameterTypes)
        {
            ReadParameter(0);
            ReadParameter(1);
            ReadParameter(2, ParameterType.Position);
        }
    }
}
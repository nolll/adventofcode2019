using System.Collections.Generic;

namespace Core.Computer.Instructions
{
    public class HaltInstruction : Instruction
    {
        public override InstructionType Type => InstructionType.Halt;

        public HaltInstruction(int[] memory, int pointer, IList<ParameterType> parameterTypes)
            : base(memory, pointer, parameterTypes)
        {
        }
    }
}
using Parsing.Core.Domain.Data.Syntax;

namespace Parsing.Core.Domain.Interfaces;

public interface IStateMachine
{
    List<Token> Parse(char[] inputString);
}
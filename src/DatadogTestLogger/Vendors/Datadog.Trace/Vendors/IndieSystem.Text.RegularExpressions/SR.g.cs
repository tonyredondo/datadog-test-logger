//------------------------------------------------------------------------------
// <auto-generated />
// This file was automatically generated by the UpdateVendors tool.
//------------------------------------------------------------------------------
#pragma warning disable CS0618, CS0649, CS1574, CS1580, CS1581, CS1584, CS1591, CS1573, CS8018, SYSLIB0011, SYSLIB0032
#pragma warning disable CS8600, CS8601, CS8602, CS8603, CS8604, CS8618, CS8620, CS8714, CS8762, CS8765, CS8766, CS8767, CS8768, CS8769, CS8612, CS8629, CS8774
#nullable enable
#if NETCOREAPP3_1_OR_GREATER
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatadogTestLogger.Vendors.Datadog.Trace.Vendors.IndieSystem.Text.RegularExpressions;

using System;


/// <summary>
///   A strongly-typed class, for hardcode messages
/// </summary>
internal partial class SR
{

    public const string Generic = @"Regular expression parser error '{0}' at offset {1}.";
    public const string AlternationHasNamedCapture = @"Alternation conditions do not capture and cannot be named.";
    public const string AlternationHasComment = @"Alternation conditions cannot be comments.";
    public const string Arg_ArrayPlusOffTooSmall = @"Destination array is not long enough to copy all the items in the collection. Check array index and length.";
    public const string ShorthandClassInCharacterRange = @"Cannot include class \\{0} in character range.";
    public const string BeginIndexNotNegative = @"Start index cannot be less than 0 or greater than input length.";
    public const string QuantifierOrCaptureGroupOutOfRange = @"Quantifier and capture group numbers must be less than or equal to Int32.MaxValue.";
    public const string CaptureGroupOfZero = @"Capture number cannot be zero.";
    public const string CountTooSmall = @"Count cannot be less than -1.";
    public const string EnumNotStarted = @"Enumeration has either not started or has already finished.";
    public const string AlternationHasMalformedCondition = @"Illegal conditional (?(...)) expression.";
    public const string IllegalDefaultRegexMatchTimeoutInAppDomain = @"AppDomain data '{0}' contains the invalid value or object '{1}' for specifying a default matching timeout for System.Text.RegularExpressions.Regex.";
    public const string UnescapedEndingBackslash = @"Illegal \\ at end of pattern.";
    public const string ReversedQuantifierRange = @"Illegal {x,y} with x > y.";
    public const string InvalidUnicodePropertyEscape = @"Incomplete \\p{X} character escape.";
    public const string CaptureGroupNameInvalid = @"Invalid group name: Group names must begin with a word character.";
    public const string LengthNotNegative = @"Length cannot be less than 0 or exceed input length.";
    public const string MalformedNamedReference = @"Malformed \\k<...> named back reference.";
    public const string AlternationHasMalformedReference = @"Conditional alternation is missing a closing parenthesis after the group number {0}.";
    public const string MalformedUnicodePropertyEscape = @"Malformed \\p{X} character escape.";
    public const string MakeException = @"Invalid pattern '{0}' at offset {1}. {2}";
    public const string MissingControlCharacter = @"Missing control character.";
    public const string NestedQuantifiersNotParenthesized = @"Nested quantifier '{0}'.";
    public const string NoResultOnFailed = @"Result cannot be called on a failed Match.";
    public const string InsufficientClosingParentheses = @"Not enough )'s.";
    public const string NotSupported_ReadOnlyCollection = @"Collection is read-only.";
    public const string PlatformNotSupported_CompileToAssembly = @"This platform does not support writing compiled regular expressions to an assembly. Use RegexGeneratorAttribute with the regular expression source generator instead.";
    public const string QuantifierAfterNothing = @"Quantifier '{0}' following nothing.";
    public const string RegexMatchTimeoutException_Occurred = @"The Regex engine has timed out while trying to match a pattern to an input string. This can occur for many reasons, including very large inputs or excessive backtracking caused by nested quantifiers, back-references and other factors.";
    public const string ReversedCharacterRange = @"[x-y] range in reverse order.";
    public const string ExclusionGroupNotLast = @"A subtraction must be the last element in a character class.";
    public const string InsufficientOrInvalidHexDigits = @"Insufficient hexadecimal digits.";
    public const string AlternationHasTooManyConditions = @"Too many | in (?()|).";
    public const string InsufficientOpeningParentheses = @"Too many )'s.";
    public const string UndefinedNumberedReference = @"Reference to undefined group number {0}.";
    public const string UndefinedNamedReference = @"Reference to undefined group name '{0}'.";
    public const string AlternationHasUndefinedReference = @"Conditional alternation refers to an undefined group number {0}.";
    public const string UnrecognizedUnicodeProperty = @"Unknown property '{0}'.";
    public const string UnrecognizedControlCharacter = @"Unrecognized control character.";
    public const string UnrecognizedEscape = @"Unrecognized escape sequence \\{0}.";
    public const string InvalidGroupingConstruct = @"Unrecognized grouping construct.";
    public const string UnterminatedBracket = @"Unterminated [] set.";
    public const string UnterminatedComment = @"Unterminated (?#...) comment.";
    public const string NotSupported_NonBacktrackingAndReplacementsWithSubstitutionsOfGroups = @"Regex replacements with substitutions of groups are not supported with RegexOptions.NonBacktracking.";
    public const string NotSupported_NonBacktrackingConflictingExpression = @"RegexOptions.NonBacktracking is not supported in conjunction with expressions containing: '{0}'.";
    public const string NotSupported_NonBacktrackingUnsafeSize = @"The specified pattern with RegexOptions.NonBacktracking could result in an automata as large as '{0}' nodes, which is larger than the configured limit of '{1}'.";
    public const string ExpressionDescription_Backreference = @"backreference (\\ number)";
    public const string ExpressionDescription_Conditional = @"captured group conditional (?( name ) yes-pattern | no-pattern ) or (?( number ) yes-pattern| no-pattern )";
    public const string ExpressionDescription_PositiveLookaround = @"positive lookahead (?= pattern) or positive lookbehind (?<= pattern)";
    public const string ExpressionDescription_NegativeLookaround = @"negative lookahead (?! pattern) or negative lookbehind (?<! pattern)";
    public const string ExpressionDescription_ContiguousMatches = @"contiguous matches (\\G)";
    public const string ExpressionDescription_AtomicSubexpressions = @"atomic subexpressions (?> pattern)";
    public const string ExpressionDescription_IfThenElse = @"test conditional (?( test-pattern ) yes-pattern | no-pattern )";
    public const string ExpressionDescription_BalancingGroup = @"balancing group (?<name1-name2>subexpression) or (?'name1-name2' subexpression)";
    public const string UsingSpanAPIsWithCompiledToAssembly = @"Searching an input span using a pre-compiled Regex assembly is not supported. Please use the string overloads or use a newer Regex implementation.";

}


#endif
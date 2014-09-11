// See http://www.codeproject.com/Articles/13922/Validate-user-input-in-Windows-Forms

namespace Options.Itboy
{
/// <summary>
/// CompareOperator is running when Validator sets "Compare" Flag,
/// Indicate how compare to another controls.
/// </summary>
public enum ValidationCompareOperator
{
    /// <summary>
    /// A comparison for checking data type only.
    /// </summary>
    DataTypeCheck,
    /// <summary>
    /// A comparison for equality.
    /// </summary>
    Equal,
    /// <summary>
    /// A comparison for greater than.
    /// </summary>
    GreaterThan,
    /// <summary>
    /// A comparison for greater than or equal to.
    /// </summary>
    GreaterThanEqual,
    /// <summary>
    ///  A comparison for less than.
    /// </summary>
    LessThan,
    /// <summary>
    ///  A comparison for less than or equal to.
    /// </summary>
    LessThanEqual,
    /// <summary>
    /// A comparison for inequality.
    /// </summary>
    NotEqual
}
}

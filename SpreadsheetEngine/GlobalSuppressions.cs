// <copyright file="GlobalSuppressions.cs" company="Logan Foster">
// 11754587
// </copyright>

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1214:Readonly fields should appear before non-readonly fields", Justification = "Unncessary warning.", Scope = "member", Target = "~F:SpreadsheetEngine.Cell.rowIndex")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Different from requirements document.", Scope = "member", Target = "~F:SpreadsheetEngine.Cell.Value")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1306:Field names should begin with lower-case letter", Justification = "Required Naming Convention", Scope = "member", Target = "~F:SpreadsheetEngine.Cell.BGColor")]

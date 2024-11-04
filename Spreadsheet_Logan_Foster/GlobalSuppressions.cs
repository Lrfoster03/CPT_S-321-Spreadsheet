// <copyright file="GlobalSuppressions.cs" company="Logan Foster">
// 11754587
// </copyright>

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
using System.Diagnostics.CodeAnalysis;

// Suppressed SA1400 due to wanting to change auto generated Main file to access modifier.
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1400:Access modifier should be declared", Justification = "Suppressed SA1400 due to wanting to change auto generated Main file to access modifier.", Scope = "member", Target = "~M:Spreadsheet_Logan_Foster.Program.Main")]

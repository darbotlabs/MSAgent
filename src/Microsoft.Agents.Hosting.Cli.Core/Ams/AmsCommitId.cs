using Microsoft.Agents.Hosting.Cli.Core.Errors;

namespace Microsoft.Agents.Hosting.Cli.Core.Ams;

/// <summary>Validates the commit ids that select one package upload of an agent.</summary>
internal static class AmsCommitId
{
    /// <summary>Reports whether a value has the 40 hexadecimal characters of a commit.</summary>
    internal static bool IsValid(string value) => value.Length == 40 && value.All(char.IsAsciiHexDigit);

    /// <summary>Returns the canonical commit id of an option value, null when it is blank, or throws a usage error when it is malformed.</summary>
    internal static string? ParseOption(string? value) => string.IsNullOrWhiteSpace(value) ? null : IsValid(value.Trim()) ? value.Trim().ToLowerInvariant() : throw new UsageException("--commit-id must be the 40 hexadecimal characters of a commit. `msagent agent commit list` shows the agent's commits.");
}

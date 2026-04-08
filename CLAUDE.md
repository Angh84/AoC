# AoC — Advent of Code

## Project Overview
C# (.NET 9.0) Advent of Code solver framework with auto-downloading inputs,
inline test infrastructure, and reflection-based solution discovery.

## Reference Files
- `doc/architecture.md` — Framework design, solution pattern, CLI commands, utilities
- `doc/labels.md` — standardized GitHub issue labels (shared across all personal repos)

## Build & Run
- `dotnet build` from repo root
- `dotnet run --project src/AoC.Runner` — run latest solution
- `dotnet run --project src/AoC.Runner run <year> <day>` — run specific day
- `dotnet run --project src/AoC.Runner benchmark [year]` — benchmark all solutions

## Solution Pattern
- One file per day: `src/AoC.Solutions/Year{YYYY}/Day{DD}.cs`
- Implements `ISolution`, decorated with `[Solution(year, day)]`
- Tests are inline properties (TestInput, ExpectedOutputPartOne, etc.)
- Parse with `StringUtils`: `SplitLines()`, `ToCharMap()`, `ToStringArray()`

## Notes
- `.aiignore` excludes `src/AoC.Solutions` — existing solutions are not visible to Claude
- Inputs are gitignored and auto-downloaded via session token
- Secrets go in `appsettings.local.json` (gitignored) — never commit tokens

## User Preferences
- Ask first when something important is uncertain
- Establish a clear plan before substantive work
- Do not add `Co-Authored-By` trailers to commit messages
- Disagree when wrong. State the correction directly.
- Do not change a correct answer because the user pushes back.
- If unsure: say "I don't know." Never guess confidently.
- Never speculate about code, files, or APIs you have not read.
- If referencing a file or function: read it first, then answer.
- Never invent file paths, function names, or API signatures.

## NEVER
- Commit input files or session tokens
- Modify the Core framework without discussing first
- Guess at puzzle input format without reading the actual input

## ALWAYS
- Return answers as strings from solve methods
- Include test cases (TestInput + ExpectedOutput) in every solution
- Run tests before claiming a solution works

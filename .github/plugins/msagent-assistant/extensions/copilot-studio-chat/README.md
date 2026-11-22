# Copilot Studio chat canvas

Chat with a Copilot Studio agent inside the GitHub Copilot app. The
`msagent-assistant` plugin provides the panel, using `msagent test ask --json`
to stream the conversation.

## Requirements

- GitHub Copilot app with extension canvases enabled and `msagent-assistant` installed.
- Node.js 22 or newer and an installed `msagent` CLI that supports
  `test ask --start-conversation` and `test ask --file`.
- The .NET and ASP.NET Core runtimes required by that CLI build.
- A selected public-cloud, production-ring CLI account with access to the agent.
  Sign in with `msagent auth create`.

## Open a chat

Ask Copilot to open a chat with your agent. Copilot helps you select the agent
and deployment, then opens the configured canvas. You can also open
**Copilot Studio chat** from the canvas menu and select **Choose an agent with Copilot**.

The canvas checks the CLI and selected account, then starts a conversation.
**Connection details** shows the account, agent, deployment, environment and tenant.
Starting a chat and sending messages are live operations that may use credits
or run agent actions.

## Use

- **Send:** Enter sends a message; Shift+Enter adds a line. The message box
  scrolls for longer drafts. Replies stream into the conversation with Markdown formatting.
- **Files:** use **Attach files** to include files with a message. Files returned
  by the agent are shown as filename buttons. Clicking an available file saves
  it to your Downloads folder and tries to show it in your file manager. It never
  opens the file. The save result appears next to the button; hover over a new
  save's result for the full path until the transcript is reloaded.
- **Inspect:** switch to **Debug** for raw activities and CLI responses, including
  tool events. Use **Find in debug data**, or a message's **View debug** menu.
  **Expand chunks** reveals grouped streaming updates.
- **Copilot access:** the Copilot app can send messages and read the full retained
  chat transcript, including messages you type in the panel.
- **New chat:** starts a fresh conversation. Start a new chat after switching
  CLI accounts or when a recovery card reports uncertain delivery.

Closing the panel clears its chat history.

On Windows, file reveal launches Explorer visibly and reports whether that
launch succeeded. Explorer's shell-broker exit code does not reliably indicate
whether a file was shown; the canvas cannot confirm the final window selection.
On Linux, file reveal opens the containing folder without selecting the file.

## Limits

- **Rendering:** HTML and Adaptive Cards are not rendered in Chat. Developers can inspect their raw content in **Debug**. Markdown images appear as alt text.
- **Messages:** up to 16,000 characters each. Some emoji and symbols count as more than one character.
- **Chat history:** up to 100 messages, counting both yours and the agent's, including opening greetings. Saved conversation data is also limited to roughly one million characters, including message metadata. Start a new chat when the history fills up.
- **Debug history:** keeps up to 1 MB of raw responses, including tool events, separately from the chat transcript. Older debug details are removed first.
- **Uploads:** attach up to five files to a message, totaling at most 10 MiB. Names must be at most 200 characters and valid on Windows; rename invalid files before attaching them.
- **Saved files:** each returned file can be up to 10 MiB. Existing files are never overwritten; a suffix is added to the name instead. Files go to the Downloads folder of the machine running the extension. If its configured Downloads folder cannot be determined, the extension uses `~/Downloads` and shows a warning. New files have owner-only permissions on Unix where supported; on macOS and Windows the extension attempts to mark them as downloaded and warns if it cannot.
- **File links:** returned files must be inline base64 data or publicly accessible HTTPS links on the standard port. Private-network links and links requiring sign-in cannot be downloaded by the canvas; ask the agent for an accessible or refreshed file.
- **File availability:** large or numerous returned files can evict older file references from memory. If a file is no longer available, ask the agent to provide it again.
- **Browser downloads:** browser-style downloads inside the canvas appear to be a limitation of the Copilot app. This extension saves files through its backend instead.
- **Restored transcripts:** files cannot be downloaded after a saved transcript has been reloaded, such as after an extension restart. Their filename buttons are disabled. A successful save's filename is remembered, but the full path, warnings, and errors are not. The saved indicator records a past save; it does not check whether the file still exists. Look for the filename in Downloads or ask the agent to return the file again. Save indicators for files saved before this feature cannot be recovered retroactively.
- **Waiting for a response:** each request has a two-minute timeout. If delivery is uncertain, the canvas prompts you to click **Start new chat**.
- **Long or complex replies:** may appear as plain text, preserving the full text.

## Data and privacy

Conversations are saved under
`$COPILOT_HOME/extensions/copilot-studio-chat/artifacts/started/`, with owner-only
permissions where supported. Debug contains raw agent responses, which may
include sensitive business data. Microsoft credentials remain in the CLI's
secure cache. Keep credentials out of messages: CLI arguments can be visible
to local process inspection.

## Contributing

From the repository root, install dependencies, build, and run the offline tests:

```text
npm ci --prefix .github/plugins/msagent-assistant/scripts
npm --prefix .github/plugins/msagent-assistant/scripts run build
npm --prefix .github/plugins/msagent-assistant/scripts test
```

Reinstall the plugin and reload extensions to try your changes.

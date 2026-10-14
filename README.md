# datadog-test-logger
Datadog Test Logger

## Log submission

Sending test output and session messages to Datadog through DirectLogSubmission is disabled by default.
To enable it, set `DD_LOGGER_DD_CIVISIBILITY_LOGS_ENABLED=true` before running the tests.
Set it to `false` to disable log submission.

The logger also respects `DD_CIVISIBILITY_LOGS_ENABLED`. The logger-specific
`DD_LOGGER_DD_CIVISIBILITY_LOGS_ENABLED` takes precedence when both are set.
This option controls log submission; test results and `<SpanTag>` metadata are still reported.

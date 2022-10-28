#!/usr/bin/env bash

bash --version 2>&1 | head -n 1

set -eo pipefail
SCRIPT_DIR=$(cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd)

###########################################################################
# CONFIGURATION
###########################################################################

BUILD_PROJECT_FILE="$SCRIPT_DIR/build/_build.csproj"
TEMP_DIRECTORY="$SCRIPT_DIR//.nuke/temp"

DOTNET_GLOBAL_FILE="$SCRIPT_DIR//global.json"
DOTNET_INSTALL_URL="https://dot.net/v1/dotnet-install.sh"
DOTNET_CHANNEL="Current"

export DOTNET_CLI_TELEMETRY_OPTOUT=1
export DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
export DOTNET_MULTILEVEL_LOOKUP=0

###########################################################################
# EXECUTION
###########################################################################

function FirstJsonValue {
    perl -nle 'print $1 if m{"'"$1"'": "([^"]+)",?}' <<< "${@:2}"
}

# If dotnet CLI is installed globally and it matches requested version, use for execution
if [ -x "$(command -v dotnet)" ] && dotnet --version &>/dev/null; then
    export DOTNET_EXE="$(command -v dotnet)"
else
    echo 'Unable to find dotnet tool with correct version'
    exit 1
fi

echo "Microsoft (R) .NET SDK version $("$DOTNET_EXE" --version)"

"$DOTNET_EXE" build "$BUILD_PROJECT_FILE" /nodeReuse:false /p:UseSharedCompilation=false -nologo -clp:NoSummary --verbosity quiet
"$DOTNET_EXE" run --project "$BUILD_PROJECT_FILE" --no-build -- "$@"

# Set the base image as the .NET 6.0 SDK (this includes the runtime)
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env

# Copy everything and publish the release (publish implicitly restores and builds)
COPY . ./
RUN dotnet publish ./UpdateStaticDataInAzureTableStorage/UpdateStaticDataInAzureTableStorage.csproj -c Release -o out --no-self-contained

# Label the container
LABEL maintainer="Tiago Almeida <tiago_almeida_rosa@hotmail.com>"
LABEL repository="https://github.com/tiagoalmeidadarosa/update-static-data-in-azure-table-storage"
LABEL homepage="https://github.com/tiagoalmeidadarosa/update-static-data-in-azure-table-storage"

# Label as GitHub action
LABEL com.github.actions.name="Update Static Data In Azure Table Storage"
LABEL com.github.actions.description="A Github action that includes data in a azure table storage from a csv file."
LABEL com.github.actions.icon="upload"
LABEL com.github.actions.color="blue"

# Relayer the .NET SDK, anew with the build output
FROM mcr.microsoft.com/dotnet/sdk:6.0
COPY --from=build-env /out .
ENTRYPOINT [ "dotnet", "/UpdateStaticDataInAzureTableStorage.dll" ]

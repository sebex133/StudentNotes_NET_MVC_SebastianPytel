FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app_dotnet
COPY . ./
WORKDIR /app_dotnet/StudentNotes_MVC_SebastianPytel
RUN dotnet publish -c Release -o released

FROM microsoft/dotnet:2.1-sdk AS runtime
WORKDIR /app_dotnet
COPY --from=build /app_dotnet/StudentNotes_MVC_SebastianPytel/released ./
ENTRYPOINT ["dotnet", "StudentNotes_MVC_SebastianPytel.dll"]
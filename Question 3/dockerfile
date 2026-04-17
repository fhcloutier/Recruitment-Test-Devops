ARG BUILD_FROM=mcr.microsoft.com/mssql/server:2019-latest

FROM ${BUILD_FROM} AS builder

ENV SA_PASSWORD=NotSoSafePassw0rd!
ENV MSSQL_PID=Express
ENV ACCEPT_EULA=Y

USER root
RUN apt-get update && apt-get install -y mssql-tools unixodbc-dev && rm -rf /var/lib/apt/lists/*
USER mssql

# Copy the data.sql file to the folder the script is using
COPY data.sql /var/opt/mssql/docker/
# Copy the restore_db script to the app folder to be executed
COPY restore_db.sh /app/

# Run the server in background (sending outputs to null) and run the restore_db script
RUN /bin/bash -c 'nohup /opt/mssql/bin/sqlservr > /dev/null 2>&1 & \
    /app/restore_db.sh'


FROM ${BUILD_FROM} AS final

ENV SA_PASSWORD=NotSoSafePassw0rd!
ENV MSSQL_PID=Express
ENV ACCEPT_EULA=Y

USER root
RUN apt-get update && apt-get install -y mssql-tools && rm -rf /var/lib/apt/lists/*
USER mssql

# Copy the previously imported data to the final image
COPY --from=builder /var/opt/mssql/data /var/opt/mssql/data
COPY entrypoint.sh /app/

HEALTHCHECK --interval=15s --timeout=10s --start-period=40s --retries=3 \
    CMD /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "${SA_PASSWORD}" -Q "SELECT 1" || exit 1

ENTRYPOINT ["/app/entrypoint.sh"]

# Jared

## Steps before running
- Add the following system environment variables for the API:
  - **JARED_API_URL**: The base URL for the API `https://localhost:7050/api/`
  - **JARED_PROD_DB_NAME**: The name of the production database.

## Steps before development
- Add the following system environment variables for the database:
  - **JARED_DB_SERVER**: The local Microsoft SQL Server instance.
  - **JARED_DB_USER**: The database server user.
  - **JARED_DB_PASSWORD**: The user's password.
  - **JARED_DEV_DB_NAME**: The name of the development database.

## Steps before integration tests
- Add the following system environment variables for the test database:
  - **JARED_SNAPSHOT_NAME**: The name of the test database snapshot.
  - **JARED_SNAPSHOT_PATH**: The location where the SQL Server has access to the snapshot (e.g., `"C:\\Program Files\\Microsoft SQL Server\\MSSQL16.MSSQLSERVER\\MSSQL\\DATA\\Jared_Tests_Snapshot.ss"`).
  - **JARED_TEST_DB_NAME**: The name of the integration test database.

## Steps before dockerization
- Add the following system environment variables for the database from the command line:
  - `$env:JARED_PROD_DB_NAME="YourDbName"`
  - `$env:JARED_DB_USER="YourDbUser"`
  - `$env:JARED_DB_PASSWORD="YourDbPassword"`
- Build and run the Docker container with the following command:
  - `docker-compose up -d --build`

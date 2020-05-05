rem IMPORTANT: Build requires that valid SqlLocalDB.msi is present in the workspace root directory
rem            The package can be downloaded using SQLServer Express installer

pushd .\..\..
docker build -t webgoatcore .
popd
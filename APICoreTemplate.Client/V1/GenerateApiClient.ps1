nswag run ./ClientApi.nswag /runtime:NetCore21
copy-item ClientApi.cs.tmp ClientApi.cs
rm ClientApi.cs.tmp
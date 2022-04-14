Extract data.zip to this folder
Folder structure should be \Data\{location_parameter}

GET S3 bucket raw & normalized files
aws s3 cp --recursive s3://tf-datapipeline-development-domains/raw_data/2022-03-06/ "C:\Users\marius.badana\source\repos\CSVParquetCompareSolution\CSVParquetCompare\Data\csv_parquet_whoisXML\csv\"

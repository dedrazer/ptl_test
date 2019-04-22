if exists(select 1 from master..sysdatabases where Name = 'ptl_test')
begin
	drop database ptl_test;
end
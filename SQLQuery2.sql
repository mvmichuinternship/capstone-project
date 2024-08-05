select * from properties
select * from propertydetails
select * from medias
select*from users
select * from OtpRecords
select * from TokenData

UPDATE users
SET phone = '7338985214'
WHERE UserEmail='mvmichuinternships@gmail.com';

UPDATE TokenData
SET phone = '7338985214'
WHERE UserEmail='mvmichuinternships@gmail.com';


delete from users where UserEmail='mridu@gmail.com';

ALTER TABLE Medias
DROP CONSTRAINT FK_Medias_Properties_PropertyPId;

ALTER TABLE Medias
ADD CONSTRAINT FK_Medias_Properties_PropertyPId
FOREIGN KEY (PropertyPId) REFERENCES Properties(PId)
ON DELETE CASCADE;

ALTER TABLE PropertyDetails
ADD CONSTRAINT FK_PropertyDetails_Properties_PId
FOREIGN KEY (PId) REFERENCES Properties(PId)
ON DELETE CASCADE on update cascade;

delete from users
delete from medias
delete from Properties
delete from PropertyDetails


drop database RealEstateApp
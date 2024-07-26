select * from properties
select * from propertydetails
select * from medias

ALTER TABLE Medias
DROP CONSTRAINT FK_Medias_Properties_PropertyPId;

ALTER TABLE Medias
ADD CONSTRAINT FK_Medias_Properties_PropertyPId
FOREIGN KEY (PropertyPId) REFERENCES Properties(PId)
ON DELETE CASCADE on update cascade;

delete from propertydetails where pid=4
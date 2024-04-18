Create TABLE Animal(
                       IdAnimal INT PRIMARY KEY IDENTITY (1,1),
                       Name NVARCHAR(200),
                       Description NVARCHAR(200) NULL,
                       Category NVARCHAR(200),
                       Area NVARCHAR(200)
)

    INSERT Into Animal
VALUES ('Animal1', 'Desc1', 'Cat1', 'Area1');
INSERT Into Animal
VALUES ('Animal2', 'Desc2', 'Cat2', 'Area2');
INSERT Into Animal
VALUES ('Animal3', 'Desc3', 'Cat3', 'Area3');
INSERT INTO Animal (Name, Description, Category, Area)
VALUES ('Luna', 'White fur, medium-sized.', 'Cat', 'North Zone');

INSERT INTO Animal (Name, Description, Category, Area)
VALUES ('Max', 'Black fur, large-sized.', 'Dog', 'South Zone');

INSERT INTO Animal (Name, Description, Category, Area)
VALUES ('Charlie', 'Grey fur, small-sized.', 'Rabbit', 'East Zone');

INSERT INTO Animal (Name, Description, Category, Area)
VALUES ('Bella', 'White feathers, large-sized.', 'Bird', 'Waterfront');

INSERT INTO Animal (Name, Description, Category, Area)
VALUES ('Lucy', 'Brown fur, large-sized.', 'Cat', 'Central Park');

INSERT INTO Animal (Name, Description, Category, Area)
VALUES ('Daisy', 'Green and yellow feathers, medium-sized.', 'Bird', 'Tropical Region');

INSERT INTO Animal (Name, Description, Category, Area)
VALUES ('Molly', 'Grey skin, very large-sized.', 'Aquatic Mammal', 'Oceanic Area');

INSERT INTO Animal (Name, Description, Category, Area)
VALUES ('Bailey', 'Brown fur, large-sized.', 'Dog', 'Guarded Lands');

INSERT INTO Animal (Name, Description, Category, Area)
VALUES ('Sophie', 'Golden fur, medium-sized.', 'Wild Cat', 'Savannah Zone');

INSERT INTO Animal (Name, Description, Category, Area)
VALUES ('Oscar', 'Brown feathers, medium-sized.', 'Bird', 'Forest Region');


Select * From Animal ORDER BY name;
CREATE TABLE Users (
                       UserID INT AUTO_INCREMENT PRIMARY KEY,
                       Username VARCHAR(255) NOT NULL UNIQUE,
                       Email VARCHAR(255) NOT NULL UNIQUE,
                       PasswordHash CHAR(64) NOT NULL,
                       CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                       LastModified TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);
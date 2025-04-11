CREATE TABLE HashedCredentials(
    UserID int Identity(1,1) PRIMARY KEY,
    Username nvarchar(200),
    Password nvarchar(200))

INSERT INTO HashedCredentials (Username, Password)
VALUES
('sarahbennett', '1000:xDT3hh8w4Dqtocj5u/jOG6A29gskdU35:8x9ILOZKjhT4uEUp/QkKEJPW5yE='),
('nickclement', '1000:1j5om/w9GjN7so5k+zaSTB5NSleADa0R:a7qucL+7oS41Hx4PgR56pe1sCic='),
('nadeemhudson', '1000:tdLYK3BlToGRL6Z9s9xkAdXwMiuHewEP:U0P2vWYdf7nQVlRFIct8E3a8skk='),
('joshwhite', '1000:lUpd7evTLNvYJwA4VzVPv8UTbTiqMETA:t8e8kug3nV+5MI3ooy88i21N1SI='),
('sharons', '1000:Jz+J7F/LvryFn04MLhC7C3JUpmM3zfXF:kGYht25u+FyGY1BI9wiGUGl/dvM='),
('marcusjohnson', '1000:CmQ5ygpkxooV6EdIj7/qRl5RfMIMh50q:2MioCDWfIvEMX5kNC1sbJ/h+DtE='),
('benfrench', '1000:16V5YyRKSwcz3R9HoDW7RH0NAZI/5q9x:cRIxRzzOiP6MrsapKLbRfkf7k84='),
('royrinehart', '1000:36Ju8poOL9YzNnYSBZWEC/XfESfyyaGF:EhYQtt5kZ/Ahd38o9Bi2g1oNTRQ='),
('ryanbucciero', '1000:azTtjKC1Bgy+HeXHhTjXxbPk+KlUSOT/:GTdXkKTg+THLmjh+EkAJSYF9Q6k='),
('BabikDmx', '1000:Z//G6n641uyPYa/F1K/9xacYw60TrOv7:8F5akYFuXBzW9Ptp9SC2pjwatWc='),
('samO', '1000:D326fZPir3gMqi5w6XVPz9rmQ+5ti4UZ:/6AoJ1JT7yDPwpo2+lzF6UsSHw0=');

-- SELECT * FROM HashedCredentials;

-- Reset: TRUNCATE TABLE HashedCredentials;

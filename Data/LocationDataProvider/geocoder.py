import json
import geopy
from geopy.geocoders import Nominatim
from geopy.extra.rate_limiter import RateLimiter

class Church:
    def __init__(self, name, street, houseNumber, postalCode, city, country, coordinats):
        self.name = name
        self.street = street
        self.houseNumber = houseNumber
        self.postalCode = postalCode
        self.city = city
        self.country = country
        self.coordinats = coordinats
        

    def updateCoordinats(self, coordinats):
        self.coordinats = coordinats
        
    def addressData(self):
        return self.street + ', ' + self.postalCode + ' ' + self.city + ', ' + self.country

churchesList = []

file_path = 'churches_raw.json'
with open(file_path, encoding="utf-8") as file:
    data_raw = json.load(file)

    
geolocator = Nominatim(user_agent="name-of-your-user-agent")
geocode = RateLimiter(geolocator.geocode, min_delay_seconds=1.5)

counter= 0
for data in data_raw:
    
    address = data['Adres'].split(", ")
    newChurch = Church(
        name=data['Nazwa'],
        street= address[0],
        houseNumber = data['Nr posesji'],
        postalCode=data['Kod pocztowy'],
        city=data['Miejscowość'],
        country='Poland',
        coordinats= [0,0]
    )
    try:
        location = geocode(newChurch.addressData())
    except geopy.exc.GeopyError:
        print('timeout :(')
    if(location is not None):
        newChurch.updateCoordinats( [location.latitude, location.longitude])
        churchesList.append(newChurch)
    counter = counter+1
    print(counter)
    with open('churches_coded.json', 'w', encoding='utf8') as jsonFile:
        json.dump([church.__dict__ for church in churchesList], jsonFile, ensure_ascii=False)




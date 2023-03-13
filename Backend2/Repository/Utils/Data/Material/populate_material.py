"""Material Populator
   Populates the material collection
"""
from sys import stdout
from datetime import datetime
from random import randint
from wonderwords import RandomWord

class MaterialPopulator:
    """Class to populate the material collection."""
    # Stores stdout so it can be restored later, don't really know if I have to do this.
    or_stdout = stdout
    # Assigns alias to RandomWord
    r = RandomWord()
    categories = [
        '640e70a13542f789e29d4fc7',
        '640e70a13542f789e29d4fc8',
        '640e70a13542f789e29d4fc9',
        '640e70a13542f789e29d4fca',
        '640e70a13542f789e29d4fcb',
        '640e70a13542f789e29d4fcc',
        '640e70a13542f789e29d4fcd',
        '640e70a13542f789e29d4fce',
        '640e70a13542f789e29d4fcf',
        '640e70a13542f789e29d4fd0'
    ]
    # { _id: { $oid: '' }, name: $ww, createdAt: { $date: "2023-03-12T17:00:00Z"}, children: [] }

    def name_me(self):
        """Returns a string with a random word."""
        return "\""+self.r.word(include_parts_of_speech=["noun","verb"])+"\""

    def to_file(self, x, y):
        """Writes output to file."""
        with open(x, "a", encoding="utf-8") as f:
            f.write(y)

    def get_address(self):
        country = ['Mexico', 'USA', 'Guatemala', 'Canada'] 
        cities = [
            ('Aguascalientes','Aguascalientes'),
            ('Nuevo León','Monterrey'),
            ('Xalisco','Guadalajara'),
            ('Cd. de México','Ciudad de México'),
            ('California','Los Angeles'),
            ('Texas','Houston'),
            ('Arizona','Phoenix'),
            ('Dpto. de Guatemala','Guatemala City'),
            ('Ontario','Ottawa')
        ]
        temp = randint(1,28)
        if (temp < 21):
            chosen_country = country[0]
            chosen_city = cities[randint(0,3)]
        elif (temp < 26):
            chosen_country = country[1]
            chosen_city = cities[randint(4,6)]
        elif(temp < 28):
            chosen_country = country[2]
            chosen_city = cities[7]
        else:
            chosen_country = country[3]
            chosen_city = cities[8]

        address = f'\u007b"street":{ self.name_me() },'
        address += f'"number":{ randint(100,5000) },'
        address += f'"colony":{ self.name_me() },'
        address += f'"postalCode":{ randint(1000,50000) },'
        address += f'"city":"{ chosen_city[0] }",'
        address += f'"state":"{ chosen_city[1] }",'
        address += f'"country":"{ chosen_country }",'
        address += '"coordinates":\u007b"x":0,"y":0\u007d'

        return address

    def main(self):
        """Do the thing."""
        output = '['
        for e in self.categories:
            break_point = randint(0,6)
            i = 0
            while i < break_point:
                val = randint(100, 999)
                date_format = "%Y-%m-%dT%H:%M:%S.%fZ"

                address = self.get_address()
                created = datetime.now()

                output += '{"name":' + self.name_me() +','
                output += '"createdAt":"' + datetime.strftime(created, date_format) + '",'
                output += '"quantity":' + str(randint(0,5)) + ','
                output += '"owner":"",'
                output += '"handler":"",'
                output += '"status":' + str(0) + ','
                output += '"boughtFor":' + str(val) + ','
                output += '"depreciation":' + str(val * .9) + ','
                output += f'"location": {address}\u007d,'
                output += f'"category": \u007b "$oid":"{e}"\u007d'
                output += '},'
                i += 1
        output += ']'
        self.to_file('material.json',output)

def main():
    """Runs the program."""
    pop = MaterialPopulator()
    pop.main()

main()

"""Material Populator
   Populates the material collection
"""
from sys import stdout
from datetime import datetime
from wonderwords import RandomWord

class MaterialPopulator:
    """Class to populate the material collection."""
    # Stores stdout so it can be restored later, don't really know if I have to do this.
    or_stdout = stdout
    # Assigns alias to RandomWord
    r = RandomWord()
    # { _id: { $oid: '' }, name: $ww, createdAt: { $date: "2023-03-12T17:00:00Z"}, children: [] }

    def name_me(self):
        """Returns a string with a random word."""
        return "\""+self.r.word(include_parts_of_speech=["noun","verb"])+"\""

    def to_file(self, x, y):
        """Writes output to file."""
        with open(x, "a", encoding="utf-8") as f:
            f.write(y)

    def main(self):
        """Do the thing."""
        output = '[\n'
        i = 0
        while i < 10:
            date_format = "%Y-%m-%dT%H:%M:%S.%fZ"
            created = datetime.now()
            output += '\t{ \"name\": ' + self.name_me() + ', \"createdAt\": \"' + datetime.strftime(created, date_format)+'\" },\n'
            i+=1
        output += ']'
        self.to_file('material.json',output)

def main():
    """Runs the program."""
    pop = MaterialPopulator()
    pop.main()

main()

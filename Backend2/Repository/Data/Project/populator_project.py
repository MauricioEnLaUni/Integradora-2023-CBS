"""Project Populator
    Generates dummy records for the database.
"""
import datetime
import random
from wonderwords import RandomWord

class Address:
    """Generates an address."""
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
    chosen_country = ''
    chosen_city = ''

    def choose(self):
        """Sets the country, state and city."""
        temp = random.randint(1,28)
        if (temp < 21):
            self.chosen_country = self.country[0]
            self.chosen_city = self.cities[random.randint(0,3)]
        elif (temp < 26):
            self.chosen_country = self.country[1]
            self.chosen_city = self.cities[random.randint(4,6)]
        elif(temp < 28):
            self.chosen_country = self.country[2]
            self.chosen_city = self.cities[7]
        else:
            self.chosen_country = self.country[3]
            self.chosen_city = self.cities[8]

    def stringify(self):
        """Generates an address as a string."""
        self.choose()
        address = f'\u007b"street":"{ RandomWord().word(include_parts_of_speech=["noun","verb"]).capitalize() }",'
        address += f'"number":{ random.randint(100,5000) },'
        address += f'"colony":"{ RandomWord().word(include_parts_of_speech=["noun","verb"]).capitalize() }",'
        address += f'"postalCode":{ random.randint(1000,50000) },'
        address += f'"city":"{ self.chosen_city[0] }",'
        address += f'"state":"{ self.chosen_city[1] }",'
        address += f'"country":"{ self.chosen_country }",'
        address += '"coordinates":\u007b"x":0,"y":0\u007d\u007d'
        return address

class Payment:
    """Generates a payment."""
    def in_list(self):
        """I'm not sure this is allowed."""
        i = 0
        break_point = random.randint(1,5)
        output = '{'
        output += f'"name":"{RandomWord().word(include_parts_of_speech=["noun","verb"]).capitalize()}",'
        output += f'"createdAt":"{datetime.datetime.strftime(datetime.datetime.now(), "%Y-%m-%dT%H:%M:%S.%fZ")}",'
        output += '"owner":"",'
        output += '"payments":['
        payment = Payment()
        while i < break_point:
            output += payment.stringify()
            if i != break_point - 1:
                output += ','
            i += 1
        output += ']'
        output += '},'
        return output

    def stringify(self):
        """Converts to string."""
        output = '\u007b'
        output += f'"name":"{RandomWord().word(include_parts_of_speech=["noun","verb"]).capitalize()}",'
        output += f'"createdAt":"{datetime.datetime.strftime(datetime.datetime.now(), "%Y-%m-%dT%H:%M:%S.%fZ")}",'
        output += f'"deadline":"{datetime.datetime.strftime(datetime.datetime.now() + datetime.timedelta(days=10), "%Y-%m-%dT%H:%M:%S.%fZ")}",'
        output += f'"amount":"{random.randint(50,5000)}",'
        output += f'"complete":{"true" if random.getrandbits(1) == 1 else "false"}'
        output += '\u007d'
        return output

class FTask:
    """Creates a task."""

    def one_task(self, cap):
        """Generates tasks as a string."""
        i = 0
        test = FTask()
        subtasks = '['
        while i < cap:
            sub = random.randint(0, cap - 1)
            subtasks += test.one_task(sub)
            if i != cap - 1:
                subtasks += ','
            i += 1
        subtasks += ']'
        address = Address()

        output = '{'
        output += f'"name":"{RandomWord().word(include_parts_of_speech=["noun","verb"]).capitalize()}",'
        output += f'"createdAt":\u007b"$date":"{datetime.datetime.strftime(datetime.datetime.now(), "%Y-%m-%dT%H:%M:%S.%fZ")}"\u007d,'
        output += f'"startDate":\u007b"$date":"{datetime.datetime.strftime(datetime.datetime.now() + datetime.timedelta(days=10), "%Y-%m-%dT%H:%M:%S.%fZ")}"\u007d,'
        output += f'"subtasks":{subtasks},'
        output += '"employees":[],'
        output += '"material":[],'
        output += f'"address":{address.stringify()}'
        output += '}'
        return output

class ProjectPopulator:
    """Class to populate the material collection."""
    def to_file(self, file, data):
        """Writes output to file."""
        with open(file, "a", encoding="utf-8") as f:
            f.write(data)

    def main(self):
        """Do the thing."""
        output = '['
        i = 0
        while i < 10:
            pay = Payment()
            payments = pay.in_list()
            task_number = random.randint(0,2)
            tasks = '['
            j = 0
            while j < task_number:
                tasks += FTask().one_task(1)
                if j != task_number - 1:
                    tasks += ','
                j += 1
            tasks += ']'
            output += '{'
            output += f'"name":"{RandomWord().word(include_parts_of_speech=["noun","verb"]).capitalize()}",'
            output += f'"createdAt":\u007b"$date":"{datetime.datetime.strftime(datetime.datetime.now(), "%Y-%m-%dT%H:%M:%S.%fZ")}"\u007d,'
            output += f'"deadline":\u007b"$date":"{datetime.datetime.strftime(datetime.datetime.now() + datetime.timedelta(days=30), "%Y-%m-%dT%H:%M:%S.%fZ")}"\u007d,'
            output += f'"account":{payments}'
            output += f'"tasks":{tasks}'
            output += '}'
            if i != 10 - 1:
                output += ','

            i += 1
        output += ']'
        self.to_file('project.json', output)

ProjectPopulator().main()

"""Salary Populator
    Generates dummy records for the database.
"""
import random

from Repository.Utils.Data.Person.fictichos_utils import Utils

class SalaryPopulator:
    """Creates a salary list."""
    utils = Utils()

    def pop(self):
        """Generates a salary string."""
        output = '{'
        output += f'"createdAt":\u007b"$date":"{self.utils.now()}"\u007d,'
        output += '"reductions":{},'
        output += f'"rate":{random.randint(60,140)},'
        output += f'"hoursWeek":{random.randint(40,48)}'
        output += '}'
        return output

    def stringify(self, break_point):
        """Creates a salary list string."""
        i = 0
        output = '['

        while i < break_point:
            output += self.pop()
            if i != break_point - 1:
                output += ','
            i += 1
        output += ']'
        return output

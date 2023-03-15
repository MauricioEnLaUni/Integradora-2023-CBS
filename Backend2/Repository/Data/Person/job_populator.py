"""
    Generates dummy records for job.
"""
import random

from Repository.Utils.Data.Person.fictichos_utils import Utils
from Repository.Utils.Data.Person.salary_populator import SalaryPopulator

class JobPopulator:
    """Generates a job string."""
    def pop(self):
        """Creates the string."""
        utils = Utils()
        salary = SalaryPopulator().stringify(random.randint(1,3))
        resp = ''
        i = 0
        while i < random.randint(1,5):
            resp += utils.word()
            i += 1

        output = '{'
        output += f'"name":"{utils.word()}"'
        output += f'"createdAt":\u007b"$date":"{utils.now()}"\u007d'
        output += f'"salaryHistory":{salary},'
        output += f'"role":"{utils.word()}",'
        output += f'"area":"{utils.word()}",'
        output += f'"responsabilities":{resp}'
        output += '}'
        return output
    
    def stringify(self, number):
        """Generates a list of jobs."""
        i = 0
        output = '['
        while i < number:
            if i != number - 1:
                output += self.pop()
                output += ','
            i += 1
        output += ']'
        return output

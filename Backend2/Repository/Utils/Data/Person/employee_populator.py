"""
    Generates dummy records for employee.
"""
import random

from Repository.Utils.Data.Person.fictichos_utils import Utils

class EmployeePopulator:
    """Generates a job string."""

    def pop(self):
        """Generates a job as a string."""
        utils = Utils()
        active = 'true' if random.randint(0,1) else 'false'
        output = '{'
        output += f'"name":"{utils.word()}",'
        output += f'"createdAt":\u007b"$date":"{utils.now()}"\u007d,'
        output += f'"active":{active},'
        output += f'"dob":\u007b"$date":"{utils.now_days(-6570)}"\u007d,'
        output += f'"rfc":"{utils.word()}"'
        output += f'"curp":"{utils.word()}"'
        

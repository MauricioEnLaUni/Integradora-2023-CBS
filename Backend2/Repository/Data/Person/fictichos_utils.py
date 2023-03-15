"""Project Populator
    Generates dummy records for the database.
"""
import datetime
from wonderwords import RandomWord

class Utils:
    """General functions for generators."""
    def word(self):
        """Returns a random word."""
        return RandomWord().word(include_parts_of_speech=["noun","verb"]).capitalize()

    def now(self):
        """Gets now as a string."""
        return datetime.datetime.strftime(datetime.datetime.now(), "%Y-%m-%dT%H:%M:%S.%fZ")

    def now_days(self, period):
        """Returns a timestamp a period from now."""
        return datetime.datetime.strftime(datetime.datetime.now() + datetime.timedelta(days=period), "%Y-%m-%dT%H:%M:%S.%fZ")

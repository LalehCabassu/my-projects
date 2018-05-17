from selenium import webdriver
from django.core import management


def before_all(context):
    # Unless we tell our test runner otherwise, set our default browser to PhantomJS
    context.browser = webdriver.Chrome()


def before_scenario(context, scenario):
    # Reset the database before each scenario
    # This means we can create, delete and edit objects within an
    # individual scenerio without these changes affecting our
    # other scenarios
    management.call_command('flush', verbosity=0, interactive=False)

    # At this stage we can (optionally) generate additional data to setup in the database.
    # For example, if we know that all of our tests require a 'SiteConfig' object,
    # we could create it here.

def after_all(context):
    # Quit our browser once we're done!
    context.browser.quit()
    context.browser = None
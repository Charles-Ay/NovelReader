import sys
from distutils.core import setup
import py2exe

sys.argv.append('py2exe')
setup(
    options = {'py2exe': {'bundle_files': 1, 'compressed': True}},
    console=['TextGetter.py'],
    #windows = [{'script': "single.py"}],
    zipfile = None,
)
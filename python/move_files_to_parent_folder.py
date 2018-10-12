from os.path import join, isfile
from os import listdir, rename
from shutil import move

i = 1
root = 'path'
for child_dir in listdir(root):
    print(child_dir)
    if child_dir.startswith('.'):
        print('# skipping ' + child_dir)
    else:
        for filename in listdir(join(root, child_dir)):
            print('## ' + filename)
            if filename.startswith('.'):
                print('### skipping ' + filename)
            else:
                print('### moving ' + filename)
                if isfile(join(root, filename)):
                    name, ext = filename.split('.')
                    i += 1
                    new_name = name + '_' + str(i) + '.' + ext
                    print('### renaming to ' + new_name)
                    rename(join(root, filename), join(root, new_name))
                move(join(root, child_dir, filename), root)

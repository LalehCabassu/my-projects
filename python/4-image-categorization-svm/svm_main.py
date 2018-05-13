
import svm


def loadDataSet(file_path):
    lines = loadFile(file_path, False)

    result = []
    for i in range(len(lines)):
        elements = [float(e) for e in lines[i].split(',')]
        result.append(elements)

    return result


def loadLabels(file_path):
    lines = loadFile(file_path, True)

    trainingLabel = []
    labelList = []
    for i in range(len(lines)):
        elements = [int(e) for e in lines[i].split(',')]
        trainingLabel.append(elements[1])
        if elements[1] not in labelList:
            labelList.append(elements[1])

    return trainingLabel, labelList


def loadFile(file_path, header):
    all_f = open(file_path, 'r')
    lines = []
    for line in all_f:
        lines.append(line.strip('\t\n\r').rstrip(','))
    all_f.close()

    if (header):
        del lines[0]

    return lines


def writeTestLabels(testLabels, file_path):
    file = open(file_path, 'a')
    file.write('Id,Prediction\n')
    for i in range(len(testLabels)):
        line = str(i + 1) + ',' + str(testLabels[i]) + '\n'
        file.write(line)
    file.close()



def svc_basic(test_examples, training_examples, training_labels, labels):
    C, gamma = 1e2, 1e-1
    clf = svm.SVC(C=C, gamma=gamma)
    clf.fit(training_examples, training_labels, labels)
    test_labels = list(clf.predict(test_examples))
    return test_labels



def svm_veriaty(test_examples, training_examples, training_labels, labels):

    data_path = './data'

    C_2d_range = [1e-2, 1, 1e2]
    gamma_2d_range = [1e-1, 1, 1e1]
    for C in C_2d_range:
        for gamma in gamma_2d_range:
            clf = svm.SVC(C=C, gamma=gamma)
            clf.fit(training_examples, training_labels, labels)
            test_labels = list(clf.predict(test_examples))
            writeTestLabels(test_labels, data_path + '/Yte_' + str(C) + '_' + str(gamma) + '.csv')



def __main__():

    data_path = './data'

    training_examples = loadDataSet(data_path + '/Xtr.csv')
    training_labels, labels = loadLabels(data_path + '/Ytr.csv')
    test_examples = loadDataSet(data_path + '/Xte.csv')

    test_labels = svc_basic(test_examples, training_examples, training_labels, labels)
    writeTestLabels(test_labels, data_path + '/Yte-svc.csv')

    #test_labels = svm_veriaty(test_examples, training_examples, training_labels)



__main__()

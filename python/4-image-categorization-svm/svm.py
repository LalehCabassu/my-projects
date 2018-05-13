import numpy as np
import numpy.linalg as la
import itertools
from collections import Counter
import cvxopt.solvers

class SVC(object):

    def __init__(self, C, gamma):
        self._C = C
        self._gamma = gamma
        self._trainer = Trainer(rbf_kernel(self._gamma), self._C)
        self._predictor = None
        self._test_examples = None
        self._training_labels = None
        self._label_pairs = None

    def fit(self, test_examples, training_labels, labels):
        self._test_examples = test_examples
        self._training_labels = training_labels
        self._label_pairs = list(itertools.combinations(range(len(labels)), 2))

    def predict(self, test_examples):

        test_labels = [[] for i in range(0, len(test_examples))]
        for pair in self._label_pairs:
            labels_ids = [i for i, x in enumerate(self._training_labels) if x in pair]
            training_examples_subset = [x for i, x in enumerate(self._test_examples) if i in labels_ids]
            training_labels_subset = []
            for l in self._training_labels:
                if l == pair[0]:
                    training_labels_subset.append(-1)
                elif l == pair[1]:
                    training_labels_subset.append(1)

            # print "pair", pair
            # print "training_examples_subset", training_examples_subset
            # print "training_labels_subset", training_labels_subset
            self._predictor = \
                self._trainer.train_one(np.array(training_examples_subset), np.array(training_labels_subset))

            # Go through the test data and update the labels of each test sample
            binary_test_labels = []
            for t in test_examples:
                binary_test_labels.append(self._predictor.predict(t))

            test_labels_subset = [pair[0] if l == -1 else pair[1] for l in binary_test_labels]
            for i in range(len(test_labels_subset)):
                test_labels[i].append(test_labels_subset[i])

        # pick the label with the most votes
        return [Counter(labels).most_common()[0][0] for labels in test_labels]


"""
The one-against-one SVC Trainer
"""
class Trainer(object):
    def __init__(self, kernel, C):
        self._kernel = kernel
        self._C = C

    def train_one(self, X, y):
        lagrange_multipliers = self._calc_lagrange_multipliers(X, y)
        return self._build_predictor(X, y, lagrange_multipliers)

    def _calc_lagrange_multipliers(self, X, y):

        samples_size, features_size = X.shape

        #K: Gram matrix
        K = np.zeros((samples_size, features_size))
        for i, x_i in enumerate(X):
            for j, x_j in enumerate(X):
                K[i, j] = self._kernel(x_i, x_j)

        """
        Try to  solves a quadratic program
            minimize    (1/2)*x'*P*x + q'*x 
            subject to  G*x <= h      
                        A*x = b.
        """
        #P is a n x n dense or sparse 'd' matrix with the lower triangular part of P stored in the lower triangle.
        P = cvxopt.matrix(np.outer(y, y) * K)

        #q is an n x 1 dense 'd' matrix.
        q = cvxopt.matrix(-1 * np.ones(samples_size))

        #G is an m x n dense or sparse 'd' matrix.
        G_standard = cvxopt.matrix(np.diag(np.ones(samples_size) * -1))
        G_slack = cvxopt.matrix(np.diag(np.ones(samples_size)))
        G = cvxopt.matrix(np.vstack((G_standard, G_slack)))

        #h is an m x 1 dense 'd' matrix.
        h_standard = cvxopt.matrix(np.zeros(samples_size))
        h_slack = cvxopt.matrix(np.ones(samples_size) * self._C)
        h = cvxopt.matrix(np.vstack((h_standard, h_slack)))

        #A is a p x n dense or sparse 'd' matrix.
        A = cvxopt.matrix(y, (1, samples_size), 'd')

        #b is a p x 1 dense 'd' matrix or None.
        b = cvxopt.matrix(0.0)

        #solution is a dictionary with keys 'status', 'x', 's', 'y', 'z', etc.
        #'x', 's', 'y', 'z' are an approximate solution of the primal and dual optimal solutions
        solution = cvxopt.solvers.qp(P, q, G, h, A, b)

        #solution['x'] contains the lagrange multipliers
        return np.ravel(solution['x'])

    def _build_predictor(self, X, y, lagrange_multipliers):
        support_vector_indices = lagrange_multipliers > 1e-5
        support_multipliers = lagrange_multipliers[support_vector_indices]
        support_vectors = X[support_vector_indices]
        support_vector_labels = y[support_vector_indices]
        bias = np.mean(
            [y_k - Predictor(
                kernel=self._kernel,
                bias=0.0,
                weights=support_multipliers,
                support_vectors=support_vectors,
                support_vector_labels=support_vector_labels).predict(x_k)
             for (y_k, x_k) in zip(support_vector_labels, support_vectors)])

        return Predictor(
            kernel=self._kernel,
            bias=bias,
            weights=support_multipliers,
            support_vectors=support_vectors,
            support_vector_labels=support_vector_labels)


"""
The one-against-one SVC Predictor
"""
class Predictor(object):
    def __init__(self,
                 kernel,
                 bias,
                 weights,
                 support_vectors,
                 support_vector_labels):
        self._kernel = kernel
        self._bias = bias
        self._weights = weights
        self._support_vectors = support_vectors
        self._support_vector_labels = support_vector_labels

    def predict(self, x):
        result = self._bias
        for z_i, x_i, y_i in zip(self._weights,
                                 self._support_vectors,
                                 self._support_vector_labels):

            result += z_i * y_i * self._kernel(x_i, x)

        return np.sign(result).item()


"""
The Gaussian Radial Basis Kernel Function (RBF)
"""
def rbf_kernel(gamma):
    def f(x, y):
        exponent = -np.sqrt(la.norm(x-y) ** 2 / (2 * gamma ** 2))
        return np.exp(exponent)
    return f
